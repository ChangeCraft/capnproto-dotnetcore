using capnpc_csharp.Tests.Properties;
using Capnp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CapnpC
{
    [TestClass]
    public class UnitTests
    {

        [TestMethod]
        public void Test00Enumerant()
        {
            var model = Load(Resources.UnitTest1_capnp);
            Assert.AreEqual("@byte", GetTypeDef(0xc8461867c409f5d4, model).Enumerants[0].Literal);
        }

        static Model.TypeDefinition GetTypeDef(ulong id, Model.SchemaModel model)
        {
            foreach (var defs in model.FilesToGenerate.Select(f => f.NestedTypes))
            {
                if (GetTypeDef(id, defs) is Model.TypeDefinition def) return def;
            }
            return null;
        }

        static Model.TypeDefinition GetTypeDef(ulong id, IEnumerable<Model.TypeDefinition> defs)
        {
            foreach (var def in defs)
            {
                if (def.Id == id) return def;
                var sub = GetTypeDef(id, def.NestedTypes);
                if (sub != null) return sub;
            }
            return null;
        }

        static Model.SchemaModel Load(byte[] data)
        {
            WireFrame segments;
            var input = new MemoryStream(data);
            using (input)
            {
                segments = Framing.ReadSegments(input);
            }
            var dec = DeserializerState.CreateRoot(segments);
            var reader = Schema.CodeGeneratorRequest.Reader.Create(dec);
            var model = Model.SchemaModel.Create(reader);
            return model;
        }
    }
}
