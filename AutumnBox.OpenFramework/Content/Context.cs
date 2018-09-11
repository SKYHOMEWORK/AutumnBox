﻿/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/6 16:48:15 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Open;
using System;

namespace AutumnBox.OpenFramework.Content
{
    /// <summary>
    /// AutumnBox开放框架上下文
    /// </summary>
    [ContextPermission(CtxPer.Normal)]
    public abstract class Context : Object
    {
        private ContextApiProvider apiWarpper;
        /// <summary>
        /// 权限
        /// </summary>
        internal CtxPer Permission
        {
            get
            {
                if (permission == CtxPer.None)
                {
                    var attr = Attribute
                    .GetCustomAttribute(GetType(),
                    typeof(ContextPermissionAttribute), true);
                    permission = (attr as ContextPermissionAttribute)?.Value ?? CtxPer.Normal;
                }
                return permission;
            }
        }
        private CtxPer permission = CtxPer.None;
        /// <summary>
        /// 日志标签
        /// </summary>
        public virtual string LoggingTag
        {
            get
            {
                return GetType().Name;
            }
        }
        /// <summary>
        /// 声音播放器
        /// </summary>
        public ISoundPlayer SoundPlayer => apiWarpper.Factory.GetSoundPlayer(this);
        /// <summary>
        /// Ux api
        /// </summary>
        public IUx Ux => apiWarpper.Ux;
        /// <summary>
        /// 日志API
        /// </summary>
        public ILogger Logger => apiWarpper.Logger;
        /// <summary>
        /// 秋之盒整体程序相关API
        /// </summary>
        public IAppManager App => apiWarpper.App;
        /// <summary>
        /// 操作系统API
        /// </summary>
        public IOSApi OperatingSystem => apiWarpper.OS;
        /// <summary>
        /// 临时文件管理器
        /// </summary>
        public ITemporaryFloder Tmp => apiWarpper.Tmp;
        /// <summary>
        /// 兼容性相关API
        /// </summary>
        public ICompApi Comp => apiWarpper.Comp;
        /// <summary>
        /// 嵌入资源提取器
        /// </summary>
        public IEmbeddedFileManager EmbFileManager => apiWarpper.Emb;
        /// <summary>
        /// 构建
        /// </summary>
        public Context()
        {
            apiWarpper = new ContextApiProvider(this);
        }
    }
}