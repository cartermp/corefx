// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Linq
{
    /// <summary>Provides functionality to evaluate queries against a specific data source wherein the type of the data is not specified.</summary>
    /// <filterpriority>2</filterpriority>
    public interface IQueryable : IEnumerable
    {
        /// <summary>Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable" />.</summary>
        /// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> that is associated with this instance of <see cref="T:System.Linq.IQueryable" />.</returns>
        Expression Expression { get; }
        /// <summary>Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="T:System.Linq.IQueryable" /> is executed.</summary>
        /// <returns>A <see cref="T:System.Type" /> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.</returns>
        Type ElementType { get; }

        // the provider that created this query
        /// <summary>Gets the query provider that is associated with this data source.</summary>
        /// <returns>The <see cref="T:System.Linq.IQueryProvider" /> that is associated with this data source.</returns>
        IQueryProvider Provider { get; }
    }

    /// <summary>Provides functionality to evaluate queries against a specific data source wherein the type of the data is known.</summary>
    /// <typeparam name="T">The type of the data in the data source.This type parameter is covariant. That is, you can use either the type you specified or any type that is more derived. For more information about covariance and contravariance, see Covariance and Contravariance in Generics.</typeparam>
    public interface IQueryable<out T> : IEnumerable<T>, IQueryable
    {
    }

    /// <summary>Defines methods to create and execute queries that are described by an <see cref="T:System.Linq.IQueryable" /> object.</summary>
    /// <filterpriority>2</filterpriority>
    public interface IQueryProvider
    {
        IQueryable CreateQuery(Expression expression);

        IQueryable<TElement> CreateQuery<TElement>(Expression expression);

        object Execute(Expression expression);

        TResult Execute<TResult>(Expression expression);
    }

    /// <summary>Represents the result of a sorting operation.</summary>
    /// <filterpriority>2</filterpriority>
    public interface IOrderedQueryable : IQueryable
    {
    }

    /// <summary>Represents the result of a sorting operation.</summary>
    /// <typeparam name="T">The type of the content of the data source.This type parameter is covariant. That is, you can use either the type you specified or any type that is more derived. For more information about covariance and contravariance, see Covariance and Contravariance in Generics.</typeparam>
    public interface IOrderedQueryable<out T> : IQueryable<T>, IOrderedQueryable
    {
    }
}
