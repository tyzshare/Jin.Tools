using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace System
{
    public static class StaticPageHelper
    {
        /// <summary>  
        ///     根据View视图生成静态页面  
        /// </summary>  
        /// <param name="htmlPath">存放静态页面所在绝对路径</param>  
        /// <param name="context">ControllerContext</param>  
        /// <param name="viewPath">视图名称</param>  
        /// <param name="masterName">模板视图名称</param>  
        /// <param name="model">参数实体模型</param>  
        /// <param name="html">返回信息</param>  
        /// <param name="isPartial">是否分布视图</param>  
        /// <returns>生成成功返回true,失败false</returns>  
        public static AjaxResult GenerateStaticPage(string viewPath,
                                                    string htmlPath,
                                                    ControllerContext context, object model = null, bool isPartial = false,
                                                    string masterName = "")
        {
            var ajaxResult = new AjaxResult();
            try
            {
                //创建存放静态页面目录                              
                if (!Directory.Exists(Path.GetDirectoryName(htmlPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(htmlPath));
                }
                //删除已有的静态页面  

                if (File.Exists(htmlPath))
                {
                    File.Delete(htmlPath);
                }
                ViewEngineResult result = null;
                if (isPartial)
                {
                    result = ViewEngines.Engines.FindPartialView(context, viewPath);
                }
                else
                {
                    result = ViewEngines.Engines.FindView(context, viewPath, masterName);
                }

                if (model != null)
                {
                    context.Controller.ViewData.Model = model;
                }

                /* 
                 * 设置临时数据字典作为静态化标识 
                 * 可以在视图上使用TempData["IsStatic"]来控制某些元素显示。 
                 */
                if (!context.Controller.TempData.ContainsKey("IsStatic"))
                {
                    context.Controller.TempData.Add("IsStatic", true);
                }

                if (result.View != null)
                {
                    using (var sw = new StringWriter())
                    {
                        var viewContext = new ViewContext(context,
                                                          result.View,
                                                          context.Controller.ViewData,
                                                          context.Controller.TempData, sw);

                        result.View.Render(viewContext, sw);

                        string body = sw.ToString();
                        File.WriteAllText(htmlPath, body, Encoding.UTF8);
                        ajaxResult.IsSucess = true;
                        ajaxResult.Body = "存放路径：" + htmlPath;
                    }
                }
                else
                {
                    ajaxResult.IsSucess = false;
                    ajaxResult.Body = "生成静态页面失败！未找到视图！";
                }
            }
            catch (IOException ex)
            {
                ajaxResult.IsSucess = false;
                ajaxResult.Body = ex.Message;
            }
            catch (Exception ex)
            {
                ajaxResult.IsSucess = false;
                ajaxResult.Body = ex.Message;
            }
            return ajaxResult;
        }
    }
    public class AjaxResult
    {
        public bool IsSucess { get; set; }
        public string Body { get; set; }
    }
}
