using System;
using System.Collections;
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
        public readonly FacadeMapping[] Members;
        public readonly Dictionary<DependencyProperty, FacadeMapping> ByViewProperty;
        static readonly Dictionary<Tuple<Type, Type>, FacadeType> All = new Dictionary<Tuple<Type, Type>, FacadeType>();

        public FacadeType(Type viewType, Type modelType)
        {
            ViewType = viewType;
            ModelType = modelType;
            var depprops = (from field in ViewType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                            where field.FieldType == typeof(DependencyProperty) && field.Name.EndsWith("Property")
                            select (DependencyProperty)field.GetValue(null)).ToList();
            var members = new List<FacadeMapping>();
            foreach (var dep in depprops)
            {
                FacadeMember member = null;
                var property = ModelType.GetProperty(dep.Name);
                if (property != null)
                    member = new FacadeProperty(property);
                var field = ModelType.GetField(dep.Name);
                if (field != null)
                    member = new FacadeField(field);
                if (member != null)
                {
                    member = FacadeObservable.Unwrap(member);
                    members.Add(FacadeCollection.TryMap(dep, member) ?? new FacadeScalar(dep, member));
                }
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
