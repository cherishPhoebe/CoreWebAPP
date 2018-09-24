using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreWebApp.Controllers
{
    public class OperationsController : Controller
    {
        private readonly OperationService _operationService;
        private readonly IOperationTransient _transientOperation;
        private readonly IOperationScoped _scopedOperation;
        private readonly IOperationSingleton _singletonOperation;
        private readonly IOperationSingletonInstance _singletonInstanceOperation;
        private readonly ILogger<OperationsController> _logger;

        public OperationsController(OperationService operationService,
            IOperationTransient transientOperation,
            IOperationScoped scopedOperation,
            IOperationSingleton singletonOperation,
            IOperationSingletonInstance singletonInstanceOperation,
            ILogger<OperationsController> logger) {
            _operationService = operationService;
            _scopedOperation = scopedOperation;
            _transientOperation = transientOperation;
            _singletonOperation = singletonOperation;
            _singletonInstanceOperation = singletonInstanceOperation;
            _logger = logger;
        }



        // GET: /<controller>/
        public IActionResult Index()
        {
            _logger.LogInformation("OperationsController Transient:" + _transientOperation.OperationId.ToString());
            _logger.LogInformation("OperationsController Scoped:" + _scopedOperation.OperationId.ToString());
            _logger.LogInformation("OperationsController Singleton:" + _singletonOperation.OperationId.ToString());
            _logger.LogInformation("OperationsController SingletonInstance:" + _singletonInstanceOperation.OperationId.ToString());
            _logger.LogInformation("OperationService Transient:" + _operationService.TransientOperation.OperationId.ToString());
            _logger.LogInformation("OperationService Scoped:" + _operationService.ScopedOperation.OperationId.ToString());
            _logger.LogInformation("OperationService Singleton:" + _operationService.SingletonOperation.OperationId.ToString());
            _logger.LogInformation("OperationService SingletonInstance:" + _operationService.SingletonInstanceOperation.OperationId.ToString());

            return View();
        }
    }
}
