﻿// -----------------------------------------------------------------------
//  <copyright file="UserLoginConfiguration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2018 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2018-06-27 4:48</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Hosting.Identity.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OSharp.Entity;


namespace OSharp.Hosting.EntityConfiguration.Identity
{
    public partial class UserLoginConfiguration : EntityTypeConfigurationBase<UserLogin, Guid>
    {
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<UserLogin> builder)
        {
#if NET5_0_OR_GREATER
            builder.HasIndex(m => new { m.LoginProvider, m.ProviderKey }).HasDatabaseName("UserLoginIndex").IsUnique();
#else
            builder.HasIndex(m => new { m.LoginProvider, m.ProviderKey }).HasName("UserLoginIndex").IsUnique();
#endif
            builder.HasOne(ul => ul.User).WithMany(u => u.UserLogins).HasForeignKey(ul => ul.UserId).IsRequired();

            EntityConfigurationAppend(builder);
        }

        /// <summary>
        /// 额外的数据映射
        /// </summary>
        partial void EntityConfigurationAppend(EntityTypeBuilder<UserLogin> builder);
    }
}