using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace RPGData
{
    public class ScriptEngineParams
    {
        public string Type;
        public string Value;

        #region Content Type Reader
        
        public class ScriptEngineParamsReader : ContentTypeReader<ScriptEngineParams>
        {
            protected override ScriptEngineParams Read(ContentReader input,
                ScriptEngineParams existingInstance)
            {
                ScriptEngineParams sc = existingInstance;
                if (sc == null)
                {
                    sc = new ScriptEngineParams();
                }

                sc.Type = input.ReadString();
                sc.Value = input.ReadString().Replace("\\n", "\n");

                return sc;
            }
        }


        #endregion

    }
}
