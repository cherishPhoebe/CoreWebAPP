using Business.Interfaces;
using System;

namespace Business.Models
{
    public class Operation : IOperation,
        IOperationTransient,
        IOperationScoped,
        IOperationSingleton,
        IOperationSingletonInstance
    {
        public Operation() : this(Guid.NewGuid()) {

        }

        public Operation(Guid id) {
            OperationId = id;
        }

        public Guid OperationId { get; private set; }
    }
}
