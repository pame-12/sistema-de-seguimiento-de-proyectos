using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTrackingSystem.Domain.Entities.Enum
{
    public enum  TaskStatus
    {
        Pending,    // Tarea pendiente
        InProgress, // Tarea en progreso
        Completed,  // Tarea completada
        Overdue     // Tarea vencida
    }
}
