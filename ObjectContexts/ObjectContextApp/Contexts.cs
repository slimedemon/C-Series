using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ObjectContextApp
{
    // SportsCar has no special contextual needs and will
    // be loaded into default context of the AppDomain.
    public class SportsCar
    {
        public SportsCar()
        {
            // Get context information and print out context ID.
            Context ctx = Thread.CurrentContext;
            Console.WriteLine("{0} object in context {1}", this.ToString(), ctx.ContextID);

            foreach (IContextProperty itfCtxProp in ctx.ContextProperties)
            {
                Console.WriteLine("-> Ctx Prop: {0}", itfCtxProp.Name);
            }
        }
    }

    // SportsCarTS demands to be loaded in
    // a synchroniztion context.
    [Synchronization]
    public class SportsCarTS: ContextBoundObject 
    {
        public SportsCarTS() 
        {
            // Get context information and print out context ID.
            Context ctx = Thread.CurrentContext;
            Console.WriteLine("{0} object in context {1}", this.ToString(), ctx.ContextID);

            foreach (IContextProperty itfCtxProp in ctx.ContextProperties)
            {
                Console.WriteLine("-> Ctx Prop: {0}", itfCtxProp.Name);
            }
        }
    }
}
