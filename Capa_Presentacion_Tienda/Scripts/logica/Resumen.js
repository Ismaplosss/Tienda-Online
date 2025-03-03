
// script de logicas

$(document).ready(function () {
    $.ajax({
      url: '@Url.Action("Listar_Consulta","Home")',
      type: 'GET', 
      dataType: "json",
      contentType: "application/json; charset=utf-8",
      success: function (data) {
          var objeto = data.data; 
   
          $('#Can_Users').text(objeto.TotalCliente);
          $('#Can_Ventas').text(objeto.TotalVenta);
          $('#Can_Producto').text(objeto.TotalProducto);
      },
      error: function () {
          alert('Error al cargar los datos');
      }
    });
     });