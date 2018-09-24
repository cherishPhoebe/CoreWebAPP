using Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public class OperationService
    {
        public IOperationTransient TransientOperation { get; }
        public IOperationScoped ScopedOperation { get; }

        public IOperationSingleton SingletonOperation { get; }

        public IOperationSingletonInstance SingletonInstanceOperation { get; }

        public OperationService(IOperationTransient transientOperation, IOperationScoped scopedOperation, IOperationSingleton singletonOperation, IOperationSingletonInstance singletonInstanceOperation) {
            TransientOperation = transientOperation;
            ScopedOperation = scopedOperation;
            SingletonOperation = singletonOperation;
            SingletonInstanceOperation = singletonInstanceOperation;
        }


    }
}
