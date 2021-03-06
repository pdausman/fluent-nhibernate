using System;
using FluentNHibernate.Mapping;
using NHibernate.UserTypes;

namespace FluentNHibernate.Conventions
{
    /// <summary>
    /// Base class for user type conventions. Create a subclass of this to automatically
    /// map all properties that the user type can be used against. Override Accept or
    /// Apply to alter the behavior.
    /// </summary>
    /// <typeparam name="TUserType">IUserType implementation</typeparam>
    public abstract class UserTypeConvention<TUserType> : IUserTypeConvention
        where TUserType : IUserType, new()
    {
        bool IConvention<IProperty>.Accept(IProperty target)
        {
            return Accept(target.PropertyType);
        }

        public virtual bool Accept(Type type)
        {
            var userType = Activator.CreateInstance<TUserType>();

            return type == userType.ReturnedType;
        }

        public virtual void Apply(IProperty target)
        {
            target.CustomTypeIs<TUserType>();
        }
    }
}