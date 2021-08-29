using Kulala.Learning.Cache.Common;
using Kulala.Learning.Cache.Interface;
using Kulala.Learning.Cache.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kulala.Learning.Cache
{
    class Program
    {
        /// <summary>
        /// 第三方数据存储和获取的地方
        /// 
        /// 过期策略：
        /// 永久有效---目前就是
        /// 绝对过期---有个时间点，超过就过期
        /// 滑动过期---多久之后过期，如果期间更新/查询/检查存在，就再次延长
        /// 
        /// 主动清理+被动清理，保证过期数据不会被查询；过期数据也不会滞留太久
        /// 
        /// 多线程操作非线程安全的容器，会造成冲突
        /// 1 线程安全容器ConcurrentDictionary
        /// 2 用lock---Add/Remove/遍历 解决问题了，但是性能怎么办呢
        ///   怎么降低影响，提升性能呢？
        ///   多个数据容器，多个锁，容器之间可以并发
        /// 1 客户端缓存-CDN缓存-反向代理缓存-本地缓存
        /// 2 本地缓存原理和手写基础实现
        /// 
        /// 系统性能优化的第一步，就是使用缓存
        /// 缓存：实际上是一种效果&目标，就是获取数据的时候，第一次获取之后找个地方存起来，后面直接用，这样一来可以提升后面每次获取数据的效率
        /// 读取配置文件的时候把信息放在静态字段，这个就是缓存
        /// 缓存是无所不在的。。
        /// 
        /// 
        /// 1 缓存数据更新和过期策略
        /// 2 多线程冲突与解决 
        /// 3 缓存类库封装和缓存应用总结
        /// 
        /// 
        /// 缓存究竟哪里用？ 满足哪些特点适合用缓存？
        /// 访问频繁+耗时耗资源+相对稳定+体积不那么大
        /// 不是说严格满足，看情况，存一次能查3次，就值得缓存(大型项目标准)
        /// 字典/省市区/配置文件
        /// 公告信息/部门/权限/用户
        /// 热搜/类别列表/产品列表
        /// 
        /// 股票价格数据/彩票开奖信息  不行，即时性要求很高
        /// 图片/视频   不行，体积太大
        /// 商品评论  可以的，虽然评论会变，但是这不重要，而且第一页一般不变
        /// 
        /// 
        /// 可以测试一下CustomCache的性能，十万/百万/千万 插入&获取&删除的性能
        /// </summary>
        static void Main(string[] args)
        {

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"获取{nameof(IDBHelper)} {i}次 {DateTime.Now.ToString("yyyyMMdd HHmmss.fff")}");
                //List<Program> programList = DBHelper.Query<Program>(123);

                List<Program> programList = null;
                IDBHelper dBHelper = new DBHelper();
                string key = $"{nameof(DBHelper)}_Query_{123}";
                //存取数据的唯一标识:1 唯一的  2 能重现
                if (!CacheManager.Contains(key))
                {
                    programList = dBHelper.Query<Program>(123);
                    CacheManager.Add(key, programList);
                }
                else
                {
                    programList = CacheManager.GetData<List<Program>>(key);
                }
            }

            Console.ReadLine();
        }
    }
}
