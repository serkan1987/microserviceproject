﻿using MicroserviceProject.Presentation.UI.Business.Model.Department.HR;
using MicroserviceProject.Presentation.UI.Business.Model.Department.IT;
using MicroserviceProject.Presentation.UI.Infrastructure.Communication.Model.Basics;
using MicroserviceProject.Presentation.UI.Infrastructure.Communication.Moderator;
using MicroserviceProject.Presentation.UI.Infrastructure.Communication.Moderator.Providers;
using MicroserviceProject.Presentation.UI.Infrastructure.Persistence.Repositories;

using Microsoft.Extensions.Caching.Memory;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicroserviceProject.Presentation.UI.WindowsForm.Dialogs.IT
{
    public partial class CreateInventoryForNewWorkerForm : Form
    {
        private readonly CredentialProvider _credentialProvider;
        private readonly IMemoryCache _memoryCache;
        private readonly RouteNameProvider _routeNameProvider;
        private readonly ServiceCommunicator _serviceCommunicator;
        private readonly ServiceRouteRepository _serviceRouteRepository;

        public CreateInventoryForNewWorkerForm(
            CredentialProvider credentialProvider,
            IMemoryCache memoryCache,
            RouteNameProvider routeNameProvider,
            ServiceCommunicator serviceCommunicator,
            ServiceRouteRepository serviceRouteRepository)
        {
            _credentialProvider = credentialProvider;
            _memoryCache = memoryCache;
            _routeNameProvider = routeNameProvider;
            _serviceCommunicator = serviceCommunicator;
            _serviceRouteRepository = serviceRouteRepository;

            CheckForIllegalCrossThreadCalls = false;

            InitializeComponent();
        }

        private void btnVazgec_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            bool success = false;

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            try
            {
                Task.Run(async delegate
                {
                    ServiceResult<InventoryModel> createInventoryServiceResult =
                                await _serviceCommunicator.Call<InventoryModel>(
                                    serviceName: _routeNameProvider.IT_CreateDefaultInventoryForNewWorker,
                                    postData: new InventoryModel()
                                    {
                                        Id = (cmbEnvanter.SelectedItem as InventoryModel).Id
                                    },
                                    queryParameters: null,
                                    cancellationToken: cancellationTokenSource.Token);

                    if (createInventoryServiceResult.IsSuccess)
                    {
                        success = true;
                        MessageBox.Show("Envanter oluşturuldu");
                    }
                    else
                        throw new Exception(createInventoryServiceResult.Error.Description);
                },
                cancellationToken: cancellationTokenSource.Token).Wait();
            }
            catch (Exception ex)
            {
                cancellationTokenSource.Cancel();
                MessageBox.Show(ex.ToString());
            }

            if (success)
            {
                this.Close();
            }
        }

        private void CreateInventoryForNewWorkerForm_Load(object sender, EventArgs e)
        {
            GetInventories();
        }


        private void GetInventories()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            cmbEnvanter.Items.Clear();

            try
            {
                Task.Run(async delegate
                {
                    ServiceResult<List<InventoryModel>> inventoryServiceResult =
                        await _serviceCommunicator.Call<List<InventoryModel>>(
                            serviceName: _routeNameProvider.IT_GetInventories,
                            postData: null,
                            queryParameters: null,
                            cancellationToken: cancellationTokenSource.Token);

                    if (inventoryServiceResult.IsSuccess)
                    {
                        foreach (var department in inventoryServiceResult.Data)
                        {
                            cmbEnvanter.Items.Add(department);
                        }
                    }
                    else
                    {
                        throw new Exception(inventoryServiceResult.Error.Description);
                    }
                },
                cancellationToken: cancellationTokenSource.Token).Wait();
            }
            catch (Exception ex)
            {
                cancellationTokenSource.Cancel();
                MessageBox.Show(ex.ToString());
            }

            if (cmbEnvanter.Items.Count > 0)
            {
                cmbEnvanter.SelectedIndex = 0;
            }
        }
    }
}