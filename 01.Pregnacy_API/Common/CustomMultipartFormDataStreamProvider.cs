using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;

[Serializable]
	public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
	{
		public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

		public override string GetLocalFileName(HttpContentHeaders headers)
		{
			return headers.ContentDisposition.FileName.Replace("\"", string.Empty);
		}
	}
