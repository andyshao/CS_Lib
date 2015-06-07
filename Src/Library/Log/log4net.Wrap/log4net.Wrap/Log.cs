﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

using global::log4net;
using global::log4net.Config;

namespace Fre.Library.Log.log4net.Wrap
{
    /// <summary>
    /// 日志辅助
    /// </summary>
    public static partial class Log
    {
        #region 字段

        /// <summary>
        /// 日志对象缓存字典
        /// </summary>
        private static Dictionary<string, ILog> _loggersCacheDictionary;

        /// <summary>
        /// 默认日志对象处理器
        /// </summary>
        private static ILog _defaultLogger;

        #endregion

        #region 属性

        /// <summary>
        /// 默认日志对象
        /// </summary>
        public static ILog Default
        {
            get
            {
                return _defaultLogger;
            }
        }

        /// <summary>
        /// 空类型日志自动查找对应的Type类型
        /// <remarks>默认为false，生产环境推荐关闭，调试可以打开</remarks>
        /// </summary>
        public static bool NullTypeAtuoDetect { get; set; }

        #endregion

        #region 初始化

        static Log()
        {
            //首先判断程序运行目录下的Config文件夹下有无配置文件
            var log4NetConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "log4net.config");
            if (!File.Exists(log4NetConfigPath))
            {
                //判断程序运行目录下有无配置文件
                log4NetConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config");
            }

            if (File.Exists(log4NetConfigPath))
            {
                XmlConfigurator.ConfigureAndWatch(new FileInfo(log4NetConfigPath));
            }
            else
            {
                //使用嵌入式配置文件
                var log4NetConfigStream =
                    typeof(Log).Assembly.GetManifestResourceStream(
                        string.Format("{0}.Config.{1}", typeof(Log).Namespace, "log4netEmbedManifest.config"));

                XmlConfigurator.Configure(log4NetConfigStream);
            }

            _loggersCacheDictionary = new Dictionary<string, ILog>();

            _defaultLogger = LogManager.GetLogger(string.Empty);
        }

        #endregion

        #region 公共方法

        #region DebugRecord

        /// <summary>
        /// 默认Debug日志
        /// </summary>
        /// <param name="message">内容</param>
        /// <param name="e">Exception</param>
        public static void Debug(object message, Exception e = null)
        {
            Debug(null, message, e);
        }

        /// <summary>
        /// 带模块日志对象名称的Debug日志
        /// </summary>
        /// <typeparam name="T">日志类型</typeparam>
        /// <param name="message">内容</param>
        /// <param name="e">Exception</param>
        public static void Debug<T>(object message, Exception e = null)
        {
            Debug(typeof(T), message, e);
        }

        /// <summary>
        /// 带模块日志对象名称的Debug日志
        /// </summary>
        /// <param name="loggerType">日志类型</param>
        /// <param name="message">内容</param>
        /// <param name="e">Exception</param>
        public static void Debug(Type loggerType, object message, Exception e = null)
        {
            if (loggerType == null && !NullTypeAtuoDetect)
            {
                if (_defaultLogger.IsDebugEnabled)
                {
                    if (e == null)
                    {
                        _defaultLogger.Debug(message);
                    }
                    else
                    {
                        _defaultLogger.Debug(message, e);
                    }
                }
            }
            else
            {
                if (loggerType == null)
                {
                    loggerType = AutoDetectType();
                }

                if (!_loggersCacheDictionary.ContainsKey(loggerType.FullName))
                {
                    _loggersCacheDictionary.Add(loggerType.FullName, LogManager.GetLogger(loggerType));
                }

                if (_loggersCacheDictionary[loggerType.FullName].IsDebugEnabled)
                {
                    if (e == null)
                    {
                        _loggersCacheDictionary[loggerType.FullName].Debug(message);
                    }
                    else
                    {
                        _loggersCacheDictionary[loggerType.FullName].Debug(message, e);
                    }
                }
            }
        }

        #endregion

        #region InfoRecord

        /// <summary>
        /// 默认Info日志
        /// </summary>
        /// <param name="message">内容</param>
        /// <param name="e">Exception</param>
        public static void Info(object message, Exception e = null)
        {
            Info(null, message, e);
        }

        /// <summary>
        /// 带模块日志对象名称的Info日志
        /// </summary>
        /// <typeparam name="T">日志类型</typeparam>
        /// <param name="message">内容</param>
        /// <param name="e">Exception</param>
        public static void Info<T>(object message, Exception e = null)
        {
            Info(typeof(T), message, e);
        }

        /// <summary>
        /// 带模块日志对象名称的Info日志
        /// </summary>
        /// <param name="loggerType">日志类型</param>
        /// <param name="message">内容</param>
        /// <param name="e">Exception</param>
        public static void Info(Type loggerType, object message, Exception e = null)
        {
            if (loggerType == null && !NullTypeAtuoDetect)
            {
                if (_defaultLogger.IsInfoEnabled)
                {
                    if (e == null)
                    {
                        _defaultLogger.Info(message);
                    }
                    else
                    {
                        _defaultLogger.Info(message, e);
                    }
                }
            }
            else
            {
                if (loggerType == null)
                {
                    loggerType = AutoDetectType();
                }

                if (!_loggersCacheDictionary.ContainsKey(loggerType.FullName))
                {
                    _loggersCacheDictionary.Add(loggerType.FullName, LogManager.GetLogger(loggerType));
                }

                if (_loggersCacheDictionary[loggerType.FullName].IsInfoEnabled)
                {
                    if (e == null)
                    {
                        _loggersCacheDictionary[loggerType.FullName].Info(message);
                    }
                    else
                    {
                        _loggersCacheDictionary[loggerType.FullName].Info(message, e);
                    }
                }
            }
        }

        #endregion

        #region WarnRecord

        /// <summary>
        /// 默认Warn日志
        /// </summary>
        /// <param name="message">内容</param>
        /// <param name="e">Exception</param>
        public static void Warn(object message, Exception e = null)
        {
            Warn(null, message, e);
        }

        /// <summary>
        /// 带模块日志对象名称的Warn日志
        /// </summary>
        /// <typeparam name="T">日志类型</typeparam>
        /// <param name="message">内容</param>
        /// <param name="e">Exception</param>
        public static void Warn<T>(object message, Exception e = null)
        {
            Warn(typeof(T), message, e);
        }

        /// <summary>
        /// 带模块日志对象名称的Warn日志
        /// </summary>
        /// <param name="loggerType">日志类型</param>
        /// <param name="message">内容</param>
        /// <param name="e">Exception</param>
        public static void Warn(Type loggerType, object message, Exception e = null)
        {
            if (loggerType == null && !NullTypeAtuoDetect)
            {
                if (_defaultLogger.IsWarnEnabled)
                {
                    if (e == null)
                    {
                        _defaultLogger.Warn(message);
                    }
                    else
                    {
                        _defaultLogger.Warn(message, e);
                    }
                }
            }
            else
            {
                if (loggerType == null)
                {
                    loggerType = AutoDetectType();
                }

                if (!_loggersCacheDictionary.ContainsKey(loggerType.FullName))
                {
                    _loggersCacheDictionary.Add(loggerType.FullName, LogManager.GetLogger(loggerType));
                }

                if (_loggersCacheDictionary[loggerType.FullName].IsWarnEnabled)
                {
                    if (e == null)
                    {
                        _loggersCacheDictionary[loggerType.FullName].Warn(message);
                    }
                    else
                    {
                        _loggersCacheDictionary[loggerType.FullName].Warn(message, e);
                    }
                }
            }
        }

        #endregion

        #region ErrorRecord

        /// <summary>
        /// 默认Error日志
        /// </summary>
        /// <param name="message">内容</param>
        /// <param name="e">Exception</param>
        public static void Error(object message, Exception e = null)
        {
            Error(null, message, e);
        }

        /// <summary>
        /// 默认Error日志，适用与Catch捕获的错误输出
        /// </summary>
        /// <param name="e">Exception</param>
        public static void Error(Exception e)
        {
            Error(null, null, e);
        }

        /// <summary>
        /// 带模块日志对象名称的Error日志
        /// </summary>
        /// <typeparam name="T">日志类型</typeparam>
        /// <param name="message">内容</param>
        /// <param name="e">Exception</param>
        public static void Error<T>(object message, Exception e = null)
        {
            Error(typeof(T), message, e);
        }

        /// <summary>
        /// 带模块日志对象名称的Error日志，适用与Catch捕获的错误输出
        /// </summary>
        /// <typeparam name="T">日志类型</typeparam>
        /// <param name="e">Exception</param>
        public static void Error<T>(Exception e)
        {
            Error(typeof(T), null, e);
        }

        /// <summary>
        /// 带模块日志对象名称的Error日志，适用与Catch捕获的错误输出
        /// </summary>
        /// <param name="loggerType">日志类型</param>
        /// <param name="e">Exception</param>
        public static void Error(Type loggerType, Exception e)
        {
            Error(loggerType, null, e);
        }

        /// <summary>
        /// 带模块日志对象名称的Error日志
        /// </summary>
        /// <param name="loggerType">日志类型</param>
        /// <param name="message">内容</param>
        /// <param name="e">Exception</param>
        public static void Error(Type loggerType, object message, Exception e = null)
        {
            if (loggerType == null && !NullTypeAtuoDetect)
            {
                if (_defaultLogger.IsErrorEnabled)
                {
                    if (e == null)
                    {
                        _defaultLogger.Error(message);
                    }
                    else
                    {
                        _defaultLogger.Error(message, e);
                    }
                }
            }
            else
            {
                if (loggerType == null)
                {
                    loggerType = AutoDetectType();
                }

                if (!_loggersCacheDictionary.ContainsKey(loggerType.FullName))
                {
                    _loggersCacheDictionary.Add(loggerType.FullName, LogManager.GetLogger(loggerType));
                }

                if (_loggersCacheDictionary[loggerType.FullName].IsErrorEnabled)
                {
                    if (e == null)
                    {
                        _loggersCacheDictionary[loggerType.FullName].Error(message);
                    }
                    else
                    {
                        _loggersCacheDictionary[loggerType.FullName].Error(message, e);
                    }
                }
            }
        }

        #endregion

        #region FatalRecord

        /// <summary>
        /// 默认Fatal日志
        /// </summary>
        /// <param name="message">内容</param>
        /// <param name="e">Exception</param>
        public static void Fatal(object message, Exception e = null)
        {
            Fatal(null, message, e);
        }

        /// <summary>
        /// 默认Fatal日志，适用与Catch捕获的错误输出
        /// </summary>
        /// <param name="e">Exception</param>
        public static void Fatal(Exception e)
        {
            Fatal(null, null, e);
        }

        /// <summary>
        /// 带模块日志对象名称的Fatal日志
        /// </summary>
        /// <typeparam name="T">日志类型</typeparam>
        /// <param name="message">内容</param>
        /// <param name="e">Exception</param>
        public static void Fatal<T>(object message, Exception e = null)
        {
            Fatal(typeof(T), message, e);
        }

        /// <summary>
        /// 带模块日志对象名称的Fatal日志，适用与Catch捕获的错误输出
        /// </summary>
        /// <typeparam name="T">日志类型</typeparam>
        /// <param name="e">Exception</param>
        public static void Fatal<T>(Exception e)
        {
            Fatal(typeof(T), null, e);
        }

        /// <summary>
        /// 带模块日志对象名称的Fatal日志，适用与Catch捕获的错误输出
        /// </summary>
        /// <param name="loggerType">日志类型</param>
        /// <param name="e">Exception</param>
        public static void Fatal(Type loggerType, Exception e)
        {
            Fatal(loggerType, null, e);
        }

        /// <summary>
        /// 带模块日志对象名称的Fatal日志
        /// </summary>
        /// <param name="loggerType">日志类型</param>
        /// <param name="message">内容</param>
        /// <param name="e">Exception</param>
        public static void Fatal(Type loggerType, object message, Exception e = null)
        {
            if (loggerType == null && !NullTypeAtuoDetect)
            {
                if (_defaultLogger.IsFatalEnabled)
                {
                    if (e == null)
                    {
                        _defaultLogger.Fatal(message);
                    }
                    else
                    {
                        _defaultLogger.Fatal(message, e);
                    }
                }
            }
            else
            {
                if (loggerType == null)
                {
                    loggerType = AutoDetectType();
                }

                if (!_loggersCacheDictionary.ContainsKey(loggerType.FullName))
                {
                    _loggersCacheDictionary.Add(loggerType.FullName, LogManager.GetLogger(loggerType));
                }

                if (_loggersCacheDictionary[loggerType.FullName].IsFatalEnabled)
                {
                    if (e == null)
                    {
                        _loggersCacheDictionary[loggerType.FullName].Fatal(message);
                    }
                    else
                    {
                        _loggersCacheDictionary[loggerType.FullName].Fatal(message, e);
                    }
                }
            }
        }

        #endregion

        #endregion

        #region 私有方法

        /// <summary>
        /// 自动探查类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private static Type AutoDetectType(int level = 0)
        {
            int thisLevel = 0;
            thisLevel += level + 2;

            var stackFrame = new StackFrame(thisLevel, false);
            var type = stackFrame.GetMethod().ReflectedType;

            if (type == typeof(Log))
            {
                return AutoDetectType(thisLevel);
            }

            return type;
        }

        #endregion
    }
}