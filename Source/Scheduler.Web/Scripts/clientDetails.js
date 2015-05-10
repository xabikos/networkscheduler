$(function () {

  var connection = $.hubConnection('http://localhost:8080/');
  var managementProxy = connection.createHubProxy('ManagementHub');

  managementProxy.on('connectedClients', function (connectedClients) {
    var clientName = $('#clientName').html();
    connectedClients.forEach(function (cl) {
      if (cl === clientName) {
        $('#clientStatus').html('Status: Connected');
      }
    });
  });

  var clientsProxy = connection.createHubProxy('ClientsHub');

  clientsProxy.on('commandExecutionInfo', function (state, data) {
    if (state === 'started') {
      toastr.success('The command with name: ' + data.Command.Name + ' was successfully sent to the client machine', 'Command execution started');
    } else if (state === 'failed') {
      toastr.error(data, 'Sending command execution failed!');
    } else if (state === 'finished') {
      switch (data.Result) {
        case 0:
          toastr.success('The command with name: ' + data.Command.Name +
                          ' was successfully executed on the client machine', 'Command executed successfully');
          break;
        case 1:
          toastr.warning('The command with name: ' + data.Command.Name +
                          ' is still pending on the client machine', 'Command is pending');
          break;
        case 2:
          toastr.error('The command with name: ' + data.Command.Name +
                          ' was executed with errors on the client machine', 'Command executed with errors');
          break;
      }
    }
  });

  connection.start()
      .done(function () {
        managementProxy.invoke('GetConnectedClients');
        clientsProxy.invoke('JoinWebApp');
      })
      .fail(function () { alert("Could not Connect!"); });


  $('a.manualCommand').on('click', function (e) {
    e.preventDefault();
    var urlChunks = e.target.pathname.split('/');
    var modelId = $(this).data('modelid');
    var commandId = urlChunks[urlChunks.length - 1];
    clientsProxy.invoke('ExecuteCommand', modelId, commandId);
  });
});