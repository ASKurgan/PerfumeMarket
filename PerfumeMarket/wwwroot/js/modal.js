function openModal(parameters) {
    const id = parameters.data;
    const url = parameters.url;
    const modalFoundQty = $('#modal');

    if (id === undefined || url === undefined) {
        alert('Упссс.... что-то пошло не так')
        return;
    }

    $.ajax({
        type: 'GET',
        url: url,
        data: { "id": id },
        success: function (response) {
            modalFoundQty.find(".modal-body").html(response);
            modalFoundQty.modal('show')
        },
        failure: function () {
            modalFoundQty.modal('hide')
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
};