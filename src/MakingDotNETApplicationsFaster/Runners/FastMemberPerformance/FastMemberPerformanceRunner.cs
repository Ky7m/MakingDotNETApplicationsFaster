using System;
using System.Reflection;
using FastMember;
using MakingDotNETApplicationsFaster.Infrastructure;

namespace MakingDotNETApplicationsFaster.Runners.FastMemberPerformance
{
    sealed class FastMemberPerformanceRunner : IRunner
    {
        public string Value { get; set; }


        public void Run()
        {
            var obj = new FastMemberPerformanceRunner();
            dynamic dlr = obj;
            var prop = typeof(FastMemberPerformanceRunner).GetProperty("Value");

            // FastMember 
            var accessor = FastMember.TypeAccessor.Create(typeof(FastMemberPerformanceRunner));
            var wrapped = FastMember.ObjectAccessor.Create(obj);

            Type type = typeof(FastMemberPerformanceRunner);

            new PerformanceTests
            {
                {_ => StaticCSharp(obj), "StaticCSharp"},
                {_ => DynamicCSharp(dlr), "DynamicCSharp"},
                {_ => PropertyInfo(prop, obj), "PropertyInfo"},
                {_ => TypeAccessor(accessor, obj), "TypeAccessor"},
                {_ => ObjectAccessor(wrapped), "ObjectAccessor"},
                //{_ => CSharpNew(), "CSharpNew"},
                //{_ => ActivatorCreateInstance(type), "ActivatorCreateInstance"},
                //{_ => TypeAccessorCreateNew(accessor), "TypeAccessorCreateNew"}
            }.Run(1000000);
        }

        static string StaticCSharp(FastMemberPerformanceRunner obj)
        {
            obj.Value = "abc";
            return obj.Value;
        }

        static string DynamicCSharp(dynamic dlr)
        {
            dlr.Value = "abc";
            return dlr.Value;
        }

        static string PropertyInfo(PropertyInfo prop, FastMemberPerformanceRunner obj)
        {
            prop.SetValue(obj, "abc", null);
            return (string)prop.GetValue(obj, null);
        }

        static string TypeAccessor(TypeAccessor accessor, FastMemberPerformanceRunner obj)
        {
            accessor[obj, "Value"] = "abc";
            return (string)accessor[obj, "Value"];
        }

        static string ObjectAccessor(ObjectAccessor wrapped)
        {
            wrapped["Value"] = "abc";
            return (string)wrapped["Value"];
        }

        static FastMemberPerformanceRunner CSharpNew()
        {
            return new FastMemberPerformanceRunner();
        }

        static object ActivatorCreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        static object TypeAccessorCreateNew(TypeAccessor accessor)
        {
            return accessor.CreateNew();
        }
    }
}
