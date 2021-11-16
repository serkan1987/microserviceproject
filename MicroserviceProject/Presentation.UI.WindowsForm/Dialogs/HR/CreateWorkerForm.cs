﻿using Communication.Http.Department.HR.Models;

using Microsoft.Extensions.Caching.Memory;

using Presentation.UI.Infrastructure.Communication.Broker.Providers;
using Presentation.UI.Infrastructure.Communication.Model.Basics;
using Presentation.UI.Infrastructure.Persistence.Repositories;
using Presentation.UI.WindowsForm.Infrastructure.Communication.Broker;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation.UI.WindowsForm.Dialogs.HR
{
    public partial class CreateWorkerForm : Form
    {
        private readonly CredentialProvider _credentialProvider;
        private readonly IMemoryCache _memoryCache;
        private readonly RouteNameProvider _routeNameProvider;
        private readonly ServiceCommunicator _serviceCommunicator;
        private readonly ServiceRouteRepository _serviceRouteRepository;

        public CreateWorkerForm(
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

        private void CreateWorkerForm_Load(object sender, EventArgs e)
        {
            GetPeople();
            GetTitles();
            GetDepartments();
            GetWorkers();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            List<WorkerModel> managers = new List<WorkerModel>();
            var managersEnumerator = clYoneticiler.CheckedItems.GetEnumerator();

            while (managersEnumerator.MoveNext())
            {
                var worker = managersEnumerator.Current;
                managers.Add(worker as WorkerModel);
            }

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            bool success = false;

            try
            {
                var worker = new WorkerModel
                {
                    Title = cmbUnvanlar.SelectedItem as TitleModel,
                    Person = cmbKisiler.SelectedItem as PersonModel,
                    Department = cmbDepartmanlar.SelectedItem as DepartmentModel,
                    FromDate = dtBaslamaTarihi.Value,
                    BankAccounts = new List<BankAccountModel>()
                    {
                         new BankAccountModel(){ IBAN = txtIBAN.Text}
                    },
                    Managers = managers
                };

                Task.Run(async delegate
                {
                    ServiceResultModel<int> createWorkerServiceResult =
                        await _serviceCommunicator.Call<int>(
                            serviceName: _routeNameProvider.HR_CreateWorker,
                            postData: worker,
                            queryParameters: null,
                            cancellationTokenSource: cancellationTokenSource);

                    if (createWorkerServiceResult.IsSuccess)
                    {
                        success = true;
                    }
                    else
                    {
                        throw new Exception(createWorkerServiceResult.ErrorModel.Description);
                    }
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

        private void btnVazgec_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetPeople()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            cmbKisiler.Items.Clear();

            try
            {
                Task.Run(async delegate
                {
                    ServiceResultModel<List<PersonModel>> personServiceResult =
                        await _serviceCommunicator.Call<List<PersonModel>>(
                            serviceName: _routeNameProvider.HR_GetPeople,
                            postData: null,
                            queryParameters: null,
                            cancellationTokenSource: cancellationTokenSource);

                    if (personServiceResult.IsSuccess)
                    {
                        foreach (var person in personServiceResult.Data)
                        {
                            cmbKisiler.Items.Add(person);
                        }
                    }
                    else
                    {
                        throw new Exception(personServiceResult.ErrorModel.Description);
                    }
                },
                cancellationToken: cancellationTokenSource.Token).Wait();
            }
            catch (Exception ex)
            {
                cancellationTokenSource.Cancel();
                MessageBox.Show(ex.ToString());
            }

            if (cmbKisiler.Items.Count > 0)
            {
                cmbKisiler.SelectedIndex = 0;
            }
        }

        private void GetTitles()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            cmbUnvanlar.Items.Clear();

            try
            {
                Task.Run(async delegate
                {
                    ServiceResultModel<List<TitleModel>> titleServiceResult =
                        await _serviceCommunicator.Call<List<TitleModel>>(
                            serviceName: _routeNameProvider.HR_GetTitles,
                            postData: null,
                            queryParameters: null,
                            cancellationTokenSource: cancellationTokenSource);

                    if (titleServiceResult.IsSuccess)
                    {
                        foreach (var title in titleServiceResult.Data)
                        {
                            cmbUnvanlar.Items.Add(title);
                        }
                    }
                    else
                    {
                        throw new Exception(titleServiceResult.ErrorModel.Description);
                    }
                },
                cancellationToken: cancellationTokenSource.Token).Wait();
            }
            catch (Exception ex)
            {
                cancellationTokenSource.Cancel();
                MessageBox.Show(ex.ToString());
            }

            if (cmbUnvanlar.Items.Count > 0)
            {
                cmbUnvanlar.SelectedIndex = 0;
            }
        }

        private void GetDepartments()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            cmbDepartmanlar.Items.Clear();

            try
            {
                Task.Run(async delegate
                {
                    ServiceResultModel<List<DepartmentModel>> departmentServiceResult =
                        await _serviceCommunicator.Call<List<DepartmentModel>>(
                            serviceName: _routeNameProvider.HR_GetDepartments,
                            postData: null,
                            queryParameters: null,
                            cancellationTokenSource: cancellationTokenSource);

                    if (departmentServiceResult.IsSuccess)
                    {
                        foreach (var department in departmentServiceResult.Data)
                        {
                            cmbDepartmanlar.Items.Add(department);
                        }
                    }
                    else
                    {
                        throw new Exception(departmentServiceResult.ErrorModel.Description);
                    }
                },
                cancellationToken: cancellationTokenSource.Token).Wait();
            }
            catch (Exception ex)
            {
                cancellationTokenSource.Cancel();
                MessageBox.Show(ex.ToString());
            }

            if (cmbDepartmanlar.Items.Count > 0)
            {
                cmbDepartmanlar.SelectedIndex = 0;
            }
        }

        private void GetWorkers()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            clYoneticiler.Items.Clear();

            try
            {
                Task.Run(async delegate
                {
                    ServiceResultModel<List<WorkerModel>> workersServiceResult =
                        await _serviceCommunicator.Call<List<WorkerModel>>(
                            serviceName: _routeNameProvider.HR_GetWorkers,
                            postData: null,
                            queryParameters: null,
                            cancellationTokenSource: cancellationTokenSource);

                    if (workersServiceResult.IsSuccess)
                    {
                        foreach (var department in workersServiceResult.Data)
                        {
                            clYoneticiler.Items.Add(department);
                        }
                    }
                    else
                    {
                        throw new Exception(workersServiceResult.ErrorModel.Description);
                    }
                },
                cancellationToken: cancellationTokenSource.Token).Wait();
            }
            catch (Exception ex)
            {
                cancellationTokenSource.Cancel();
                MessageBox.Show(ex.ToString());
            }

            if (clYoneticiler.Items.Count > 0)
            {
                clYoneticiler.SelectedIndex = 0;
            }
        }
    }
}
