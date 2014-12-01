using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assisticant.Facades
{
    class FacadeType
    {
        public readonly Type ViewType;
        public readonly Type ModelType;
        public readonly FacadeMember[] Members;
        public readonly Dictionary<DependencyProperty, FacadeMember> ByViewProperty;
        static readonly Dictionary<Tuple<Type, Type>, FacadeType> All = new Dictionary<Tuple<Type, Type>, FacadeType>();

        public FacadeType(Type viewType, Type modelType)
        {
            ViewType = viewType;
            ModelType = modelType;
            var depprops = (from field in ViewType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                            where field.FieldType == typeof(DependencyProperty) && field.Name.EndsWith("Property")
                            select (DependencyProperty)field.GetValue(null)).ToList();
            var members = new List<FacadeMember>();
            foreach (var dep in depprops)
            {
                FacadeMember member = null;
                var property = ModelType.GetProperty(dep.Name);
                if (property != null)
                    member = new FacadeMember(dep, property);
                var field = ModelType.GetField(dep.Name);
                if (field != null)
                    member = new FacadeMember(dep, field);
                if (member != null)
                    members.Add(member);
            }
            Members = members.ToArray();
            ByViewProperty = members.ToDictionary(m => m.ViewProperty);
        }

        public static FacadeType Create(Type viewType, Type modelType)
        {
            var key = Tuple.Create(viewType, modelType);
            FacadeType facadeType;
            if (!All.TryGetValue(key, out facadeType))
                All[key] = facadeType = new FacadeType(viewType, modelType);
            return facadeType;
        }
    }
}
