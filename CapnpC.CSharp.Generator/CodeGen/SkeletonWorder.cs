﻿using System;

namespace CapnpC.CSharp.Generator.CodeGen
{
    class SkeletonWorder : Capnp.Rpc.Skeleton<object>
    {
        public override ulong InterfaceId => throw new NotImplementedException();

        public const string SetMethodTableName = nameof(SkeletonWorder.SetMethodTable);
        public const string ImplName = nameof(SkeletonWorder.Impl);
    }
}
