using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;

namespace webApiRol
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            // Esta es la implementación de un trabajador de ejemplo. Reemplácela por su lógica.
            Trace.TraceInformation("webApiRol entry point called", "Information");

            while (true)
            {
                Thread.Sleep(10000);
                Trace.TraceInformation("Working", "Information");
            }
        }

        public override bool OnStart()
        {
            // Establecer el número máximo de conexiones simultáneas 
            ServicePointManager.DefaultConnectionLimit = 12;

            // Para obtener información sobre cómo administrar los cambios de configuración
            // consulte el tema de MSDN en http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
