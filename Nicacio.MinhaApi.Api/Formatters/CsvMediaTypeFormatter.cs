using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;

namespace Nicacio.MinhaApi.Api.Formatters
{
    public class CsvMediaTypeFormatter : BufferedMediaTypeFormatter
    {
        /// <summary>
        /// atribuindo a possibilidade de utilizar o csv no accept
        /// </summary>
        public CsvMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
        }
        public override bool CanReadType(Type type)
        {
            return false;
        }

        /// <summary>
        /// método que irar "escrever" no response
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public override bool CanWriteType(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            return true;
        }

        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            using(StreamWriter writer = new StreamWriter(writeStream))
            {
                IEnumerable enumerable = value as IEnumerable;
                if (enumerable == null)
                {
                    writer.WriteLine(string.Join(";", GetPropertyName(type)));
                    WriteElement(value, writer);
                }
                else
                {
                    Type dtoType = value.GetType().GetGenericArguments()[0];
                    writer.WriteLine(string.Join(";", GetPropertyName(dtoType)));
                    foreach (var item in enumerable)
                    {
                        WriteElement(item, writer);
                    }
                }
            }
        }
        private IEnumerable<string> GetPropertyName(Type tipo)
        {
            return tipo.GetProperties().Select(s => s.Name).ToList();
        }
        private void WriteElement(object item, StreamWriter writer)
        {
            string value = string.Empty;
            foreach (string property in GetPropertyName(item.GetType()))
            {
                var propertyValue = item.GetType().GetProperty(property).GetValue(item);
                if (propertyValue != null) ;
                    value += propertyValue.ToString();
                value += ";";
            }
            writer.WriteLine(value.Substring(0, value.Length - 2));
        }

    }
}