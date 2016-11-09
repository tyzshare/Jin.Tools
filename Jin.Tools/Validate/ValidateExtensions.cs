using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;

namespace System
{
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public static class ValidateExtensions
    {



        #region caches

        static readonly ConcurrentDictionary<Type, Action<IValidate>> _validateMap = new ConcurrentDictionary<Type, Action<IValidate>>();

        #endregion

        /// <summary>
        /// 校验
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validate"></param>
        public static void Validate<T>(this T validate) where T : class, IValidate
        {
            var factory = _validateMap.GetOrAdd(typeof(T), type => { return CreateValidateExpression<T>(); });
            factory(validate);
        }

        #region private methods
        /// <summary>
        /// 创建一个验证委托
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        static Action<IValidate> CreateValidateExpression<T>() where T : class, IValidate
        {

            List<Expression> bindings = new List<Expression>();

            Type sourceType = typeof(T);

            //用于lamba表达式的参数

            ParameterExpression objectInstance = Expression.Parameter(typeof(IValidate), "sourceInstance");

            //转换为具体类型
            var sourceInstance = Expression.Convert(objectInstance, typeof(T));

            //得到所有公共属性

            var properties = typeof(T).GetProperties();

            //校验方法
            var validationMethod = typeof(ValidationAttribute).GetMethod("IsValid", new Type[] { typeof(object) });

            //错误对象构造函数
            var exceptionConstructor = typeof(ValidationException).GetConstructor(new Type[] { typeof(ValidationResult), typeof(ValidationAttribute), typeof(object) });

            //验证详情构造函数
            var validationResultConstructor = typeof(ValidationResult).GetConstructor(new Type[] { typeof(string), typeof(IEnumerable<string>) });

            var formatErrorMessageMethod = typeof(ValidationAttribute).GetMethod("FormatErrorMessage", new Type[] { typeof(string) });

            foreach (var item in properties)
            {
                if (item.GetMethod == null)
                {
                    continue;
                }
                //获取验证特性
                var validationAttributes = item.GetCustomAttributes<ValidationAttribute>();
                if (validationAttributes.Count() == 0)
                {
                    continue;
                }

                foreach (var validationAttribute in validationAttributes)
                {
                    //创建表达式

                    Expression validationAttributeExperssion = Expression.Constant(validationAttribute, typeof(ValidationAttribute));

                    //获取属性值

                    Expression propertyExperssion = Expression.Property(sourceInstance, item.GetMethod);

                    //转换成object
                    propertyExperssion = Expression.Convert(propertyExperssion, typeof(object));

                    //校验表达式

                    Expression validationExpession = Expression.Call(validationAttributeExperssion, validationMethod, propertyExperssion);


                    //错误消息表达式

                    //var errorMessageExperssion = Expression.Property(validationAttributeExperssion, typeof(ValidationAttribute).GetProperty("ErrorMessage").GetMethod);

                    var errorMessageExperssion = Expression.Call(validationAttributeExperssion, formatErrorMessageMethod, Expression.Constant(item.Name));


                    //封装一个ValidationResult

                    var errorValidationResultExpression = Expression.New(validationResultConstructor, errorMessageExperssion, Expression.NewArrayInit(typeof(string), Expression.Constant(item.Name, typeof(string))));

                    //具体要抛出的异常
                    Expression throwExpression = Expression.New(exceptionConstructor, errorValidationResultExpression, validationAttributeExperssion, propertyExperssion);

                    //表达式body

                    var validationResultExpression = Expression.IfThen(Expression.IsFalse(validationExpession), Expression.Throw(throwExpression));

                    //加入表达式集合

                    bindings.Add(validationResultExpression);

                }

            }

            //必须要有个默认的
            if (bindings.Count == 0)
            {
                bindings.Add(Expression.Empty());
            }

            //绑定代码块

            var body = Expression.Block(
                 bindings
                 );

            //创建委托
            return Expression.Lambda<Action<IValidate>>(body, objectInstance).Compile();
        }

        #endregion

    }
}
