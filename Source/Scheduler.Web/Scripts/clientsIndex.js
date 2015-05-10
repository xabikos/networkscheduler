$(function () {

  var clientsTable = $('#clients').DataTable({
    "fnRowCallback": function (nRow, aData, iDisplayIndex) {
      $('td:eq(0)', nRow).html(iDisplayIndex);
      if (aData[3] === "Connected") {
        $('td:eq(3)', nRow).html('Connected');
      } else {
        $('td:eq(3)', nRow).html('Disconnected');
      }
      $('td:eq(4)', nRow).html('<a class="btn btn-default" href="/clients/details/' + aData[1] + '" role="button">Manage</a>');
    }
  });

  var connection = $.hubConnection('http://localhost:8080/');
  var proxy = connection.createHubProxy('ManagementHub');

  proxy.on('clientAdded', function (clientData) {
    clientsTable.row.add([0, clientData.name, clientData.ipAddress, 'Connected', '']).draw();
  });

  proxy.on('clientDisconected', function (clientName) {
    $("tr:contains('" + clientName + "')").each(function () {
      $('td:eq(3)', this).html('Disconnected');
    });
  });

  proxy.on('connectedClients', function (connectedClients) {
    connectedClients.forEach(function (cl) {
      $("tr:contains('" + cl + "')").each(function () {
        $('td:eq(3)', this).html('Connected');
      });
    });
  });

  connection.start()
      .done(function () {
        proxy.invoke('GetConnectedClients');
      })
      .fail(function () { alert("Could not Connect!"); });
});