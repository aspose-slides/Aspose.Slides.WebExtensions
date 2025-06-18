// Copyright (c) 2001-2020 Aspose Pty Ltd. All Rights Reserved.

using System;
using System.Collections.Generic;
using Aspose.Slides.Export.Web;
using System.Drawing;
using System.Linq;
using RazorEngineCore;

namespace Aspose.Slides.WebExtensions
{
    class RazorEngineService : IRazorEngineService
    {
        public void AddTemplate(string key, string template)
        {
            m_commonParts.Add(key, template);
        }
        public void Compile(string key, Type modelType) { }
        public string Run(string key, Type type, object model)
        {
            var template = new CompiledTemplate(
                GetCompiledOrCashed(m_commonParts[key]),
                m_commonParts.ToDictionary(k => k.Key, v => GetCompiledOrCashed(v.Value)));

            return template.Run(model);
        }
        IRazorEngineCompiledTemplate<TemplateBase> GetCompiledOrCashed(string content)
        {
            IRazorEngineCompiledTemplate<TemplateBase> result = null;
            if (m_compiledCash.TryGetValue(content, out result)) return result;
            return m_compiledCash[content] = m_razorEngine.Compile<TemplateBase>(content, builder =>
            {
                builder.AddUsing("System");
                builder.AddUsing("System.Drawing");
                builder.AddUsing("System.Collections");
                builder.AddUsing("System.Collections.Generic");
                builder.AddUsing("Aspose.Slides");
                builder.AddUsing("Aspose.Slides.Export");
                builder.AddUsing("Aspose.Slides.Export.Web");
                builder.AddUsing("Aspose.Slides.WebExtensions");
                builder.AddUsing("Aspose.Slides.WebExtensions.Helpers");

                builder.AddAssemblyReference(typeof(Presentation));
                builder.AddAssemblyReference(typeof(SizeF));
                builder.AddAssemblyReference(typeof(FontStyle));
                builder.AddAssemblyReference(typeof(Dictionary<,>));
                builder.AddAssemblyReference(typeof(TemplateContext<>));
                builder.AddAssemblyReferenceByName("System.Drawing.Common");
                builder.AddAssemblyReference(GetType());
                try { builder.AddAssemblyReferenceByName("System.Collections"); }
                catch { }
            });
        }
        internal static IRazorEngineService Create() => new RazorEngineService();

        IRazorEngine m_razorEngine = new RazorEngine();
        IDictionary<string, string> m_commonParts = new Dictionary<string, string>();
        IDictionary<string, IRazorEngineCompiledTemplate<TemplateBase>> m_compiledCash = new Dictionary<string, IRazorEngineCompiledTemplate<TemplateBase>>();
    }
}