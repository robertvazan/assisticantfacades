using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assisticant.Facades
{
    class FacadeCollection : FacadeMapping
    {
        readonly ConditionalWeakTable<object, CollectionChangeHandler> ChangeHandlerProperty = new ConditionalWeakTable<object, CollectionChangeHandler>();
        readonly Func<object, IList> TargetConverter;

        FacadeCollection(DependencyProperty controlProp, FacadeMember modelProp, Func<object, IList> targetConverter) : base(controlProp, modelProp) { TargetConverter = targetConverter; }

        public static FacadeMapping TryMap(DependencyProperty controlProp, FacadeMember modelProp)
        {
            if (typeof(IList).IsAssignableFrom(modelProp.MemberType))
                return new FacadeCollection(controlProp, modelProp, obj => obj as IList);
            var iface = modelProp.MemberType.GetInterfaces().FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IList<>));
            if (iface != null)
            {
                var adaptorType = typeof(FacadeListAdaptor<>).MakeGenericType(iface.GetGenericArguments()[0]);
                return new FacadeCollection(controlProp, modelProp, obj => obj != null ? (IList)Activator.CreateInstance(adaptorType, obj) : null);
            }
            return null;
        }

        public override void Update(object model, object value)
        {
            CollectionChangeHandler handler;
            if (ChangeHandlerProperty.TryGetValue(model, out handler))
            {
                handler.Dispose();
                ChangeHandlerProperty.Remove(model);
            }
            var target = TargetConverter(ModelProperty.GetValue(model));
            var collection = value as INotifyCollectionChanged;
            if (target != null && collection != null)
            {
                handler = new CollectionChangeHandler(collection, target);
                ChangeHandlerProperty.Add(model, handler);
                handler.Register();
            }
            var source = value as IList;
            if (source != null && target != null)
                UpdateItems(source, target);
        }

        static void UpdateItems(IList source, IList target)
        {
            if (target != null)
            {
                for (int i = 0; i < source.Count; ++i)
                {
                    if (target.Count <= i)
                        target.Add(source[i]);
                    else if (source[i] != target[i])
                        target[i] = source[i];
                }
                for (int i = target.Count - 1; i >= source.Count; ++i)
                    target.RemoveAt(i);
            }
        }

        class CollectionChangeHandler
        {
            readonly INotifyCollectionChanged Collection;
            readonly IList Target;

            public CollectionChangeHandler(INotifyCollectionChanged collection, IList target)
            {
                Collection = collection;
                Target = target;
            }

            public void Register() { Collection.CollectionChanged += Handle; }

            void Handle(object sender, NotifyCollectionChangedEventArgs e)
            {
                var source = sender as IList;
                if (source != null)
                    UpdateItems(source, Target);
            }

            public void Dispose()
            {
                Collection.CollectionChanged -= Handle;
            }
        }
    }
}
