$(document).ready(function () {

    mostrarModal = (url, title) => {
        //alert("Ingresa a la función");
        //alert(url);
        /*alert(title);*/
        $.ajax({
            type: 'GET',
            url: url,
            success: function (res) {
                if (res.isValid == false) {
                    alertify.set('notifier', 'position', 'top-right');
                    if (res.tipoError == "error")
                        alertify.error(res.mensaje);
                } else {
                    $('#form-modal .modal-body').html(res);
                    $('#form-modal .modal-title').html(title);
                    $('#form-modal').modal('show');
                }
            }
        })
        //to prevent default form submit event
        return false;
    }

});