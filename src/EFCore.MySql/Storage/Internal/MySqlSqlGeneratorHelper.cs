﻿// Copyright (c) Pomelo Foundation. All rights reserved.
// Licensed under the MIT. See LICENSE in the project root for license information.

using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Pomelo.EntityFrameworkCore.MySql.Storage.Internal
{
    public class MySqlSqlGenerationHelper : RelationalSqlGenerationHelper
    {
        public MySqlSqlGenerationHelper([NotNull] RelationalSqlGenerationHelperDependencies dependencies)
            : base(dependencies)
        {
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public override string EscapeIdentifier(string identifier)
            => Check.NotEmpty(identifier, nameof(identifier)).Replace("`", "``");

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public override void EscapeIdentifier(StringBuilder builder, string identifier)
        {
            Check.NotEmpty(identifier, nameof(identifier));

            var initialLength = builder.Length;
            builder.Append(identifier);
            builder.Replace("`", "``", initialLength, identifier.Length);
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public override string DelimitIdentifier(string identifier)
            => $"`{EscapeIdentifier(Check.NotEmpty(identifier, nameof(identifier)))}`"; // Interpolation okay; strings
        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public override void DelimitIdentifier(StringBuilder builder, string identifier)
        {
            Check.NotEmpty(identifier, nameof(identifier));
            builder.Append('`');
            EscapeIdentifier(builder, identifier);
            builder.Append('`');
        }
    }
}
