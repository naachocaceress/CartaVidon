
// Tabla --

$(document).ready(function () {
    inicializarTabla('#tblRubros');
    inicializarTabla('#tblEtiquetas');
    inicializarTabla('#tblSubRubros');
});

function inicializarTabla(tablaId) {
    var table = $(tablaId).DataTable({
        "paging": false, // Desactiva la paginación
        "scrollX": true, // Habilita el scroll horizontal
        "language": {
            "decimal": "",
            "emptyTable": "No hay información",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 a 0 de 0 Entradas",
            "infoFiltered": "(filtrado de _MAX_ entradas en total)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar",
            "zeroRecords": "Sin resultados encontrados"
        }
    });
}

// Carga --

function handleForm(formId, tableId, buttonId, existId, rowTemplate, toastMessage) {
    var table = $(tableId).DataTable();
    $(formId).on('submit', function (e) {
        $(buttonId + ' .button-text').hide();
        $(buttonId + ' .spinner-border').show();
        $(existId).hide();
        e.preventDefault();
        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: $(this).serialize(),
            success: function (data) {
                if (data.error) {
                    $(existId).show();
                } else {
                    var newRow = rowTemplate(data);
                    $(tableId + ' tbody').append(newRow);
                    table.row.add($(newRow)).draw();
                    $('#rubroName3').html(toastMessage(data));
                    $('#liveToast').toast('show');
                }
                $(buttonId + ' .button-text').show();
                $(buttonId + ' .spinner-border').hide();
            }
        });
    });
}

$(document).ready(function () {
    handleForm('#myForm', '#tblRubros', '#myButton', '#yaExiste',
        function (data) {
            $('#nombreRubro').val('');
            var newRow = '<tr data-id="' + data.idRubro + '"><td>' + data.idRubro + '</td><td class="nombre-rubro">' + data.nombre + '</td><td class="fixed-column"><div class="btn-group"><a class="btn btn-outline-primary btn-sm edit-btn-rubro" data-id="' + data.idRubro + '" data-name="' + data.nombre + '"><svg width="16" height="16"><use href="#icono-editar" /></svg></a><a class="btn btn-outline-danger btn-sm delete-btn-rubro" data-id="' + data.idRubro + '" data-name="' + data.nombre + '"><svg width="16" height="16"><use href="#icono-eliminar" /></svg></a></div></td></tr>';
            // Agrega el nuevo rubro al select
            $('#Subrubro_IdRubro').append(new Option(data.nombre, data.idRubro));
            return newRow;
        },
        function (data) {
            return "el rubro " + "<strong>" + data.nombre + "</strong>";
        }
    );

    handleForm('#myForm2', '#tblSubRubros', '#myButton2', '#yaExiste2',
        function (data) {
            $('#nombreSubrubro').val('');
            return '<tr data-id="' + data.idSubrubro + '"><td>' + data.idSubrubro + '</td><td class="nombre-rubro">' + data.nombre + '</td><td class="idRubro-rubro"> ' + data.rubroNombre + '</td><td class="fixed-column"><div class="btn-group"><a class="btn btn-outline-primary btn-sm edit-btn-subrubro" data-id="' + data.idSubrubro + '" data-name="' + data.nombre + '" data-rubro="' + data.idRubro + '"><svg width="16" height="16"><use href="#icono-editar" /></svg></a><a class="btn btn-outline-danger btn-sm delete-btn-subrubro" data-id="' + data.idSubrubro + '" data-name="' + data.nombre + '"><svg width="16" height="16"><use href="#icono-eliminar" /></svg></a></div></td></tr>';
        },
        function (data) {
            return "el subrubro " + "<strong>" + data.nombre + "</strong>";
        }
    );

    handleForm('#myForm3', '#tblEtiquetas', '#myButton3', '#yaExiste3',
        function (data) {
            $('#nombreEtiqueta').val('');
            return '<tr data-id="' + data.idEtiqueta + '"><td>' + data.idEtiqueta + '</td><td class="nombre-rubro">' + data.nombre + '</td><td class="fixed-column"><div class="btn-group"><a class="btn btn-outline-primary btn-sm edit-btn-etiqueta" data-id="' + data.idEtiqueta + '" data-name="' + data.nombre + '"><svg width="16" height="16"><use href="#icono-editar" /></svg></a><a class="btn btn-outline-danger btn-sm delete-btn-etiqueta" data-id="' + data.idEtiqueta + '" data-name="' + data.nombre + '"><svg width="16" height="16"><use href="#icono-eliminar" /></svg></a></div></td></tr>';
        },
        function (data) {
            return "la etiqueta " + "<strong>" + data.nombre + "</strong>";
        }
    );
});

// Eliminar --

$(document).ready(function () {
    var config = [
        {
            tabla: '#tblRubros',
            btn: '.delete-btn-rubro',
            url: '/Carta/Borrar',
            id: 'IdRubro',
            name: 'el rubro',
            msg: 'Ten en cuenta que los subrubros quedarán sin rubro en caso de que este sea el suyo.',
            entityType: 'Rubro'
        },
        {
            tabla: '#tblSubRubros',
            btn: '.delete-btn-subrubro',
            url: '/Carta/Borrar',
            id: 'IdSubrubro',
            name: 'el subrubro',
            msg: 'Ten en cuenta que todos los articulos quedarán sin subrubro en caso de que este sea el suyo.',
            entityType: 'Subrubro'
        },
        {
            tabla: '#tblEtiquetas',
            btn: '.delete-btn-etiqueta',
            url: '/Carta/Borrar',
            id: 'IdEtiqueta',
            name: 'la etiqueta',
            msg: 'Ten en cuenta que todos los articulos quedarán sin etiqueta en caso de que este sea el suyo.',
            entityType: 'Etiqueta'
        }
    ];

    config.forEach(function (item) {
        var tabla = $(item.tabla).DataTable();

        $(document).on('click', item.btn, function () {
            var id = $(this).data('id');
            var name = $(this).data('name');
            $('#confirmModal').data('id', id).modal('show');
            $('#rubroName').text(item.name + ": " + name);
            $('#confirmModal .modal-body').html('<p>' + item.msg + '</p>');
            $('#confirmModal .modal-footer').html('<button type="button" class="btn btn-primary w-15" id="confirmDelete"><span class="spinner-border spinner-border-sm" role="status" aria-hidden="true" style="display: none;"></span><span class="btn-text">Aceptar</span></button><button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>');

            $('#confirmDelete').click(function () {
                var id = $('#confirmModal').data('id');
                var btn = $(this);
                btn.find('.spinner-border').show();
                btn.find('.btn-text').hide();
                btn.prop('disabled', true);
                $.ajax({
                    url: item.url,
                    type: 'POST',
                    data: { id: id, entityType: item.entityType },
                    success: function () {

                        var fila = $(item.tabla).find('tr[data-id="' + id + '"]');
                        tabla.row(fila).remove().draw();

                        $(tabla).dataTable().fnDestroy();
                        inicializarTabla(tabla);

                        if (item.tabla == '#tblRubros') {
                            $.ajax({
                                url: '/Carta/GetSubrubros',
                                method: 'GET',
                                success: function (data) {
                                    // Obtén la instancia de DataTables
                                    var table = $('#tblSubRubros').DataTable();

                                    // Vacía la tabla
                                    table.clear();

                                    // Agrega los nuevos datos a la tabla
                                    data.forEach(function (item) {
                                        table.row.add([
                                            item.idSubrubro,
                                            item.nombre,
                                            item.rubroNombre,
                                            '<div class="btn-group"><a class="btn btn-outline-primary btn-sm edit-btn-subrubro" data-id="' + item.idSubrubro + '" data-name="' + item.nombre + '" data-rubro="' + item.idSubrubro + '">Editar</a><a class="btn btn-outline-danger btn-sm delete-btn-subrubro" data-id="' + item.idSubrubro + '" data-name="' + item.nombre + '">Borrar</a></div>'
                                        ]);
                                    });

                                    // Dibuja la tabla con los nuevos datos
                                    table.draw();
                                }
                            });
                        }

                        $('#confirmModal .modal-body').html('<div class="form-group text-center p-4"><svg width="29" height="29"><use href="#icono-confirmar" /></svg> Borrado correctamente!</div>');
                        setTimeout(function () {
                            $('#confirmModal').modal('hide');
                            $('#confirmModal .modal-body').html('<p>' + item.msg + '</p>');
                            btn.find('.spinner-border').hide();
                            btn.find('.btn-text').show();
                            btn.prop('disabled', false);
                        }, 1000);
                    },
                    error: function () {
                        alert("Error al borrar " + item.name + ".");
                        btn.find('.spinner-border').hide();
                        btn.find('.btn-text').show();
                        btn.prop('disabled', false);
                    }
                });
            });
        });
    });
});

// Editar --

$(document).ready(function () {
    function editModal(editType, id, name, url, tableName, btnClass, dataName, rubro) {
        $('#editModal').data('id', id).modal('show');
        $('#editModal .modal-header').html('<h5 class="modal-title">Editar ' + editType + '</h5>');
        $('#editModal .modal-body').html('<div class="form-group"> <label for="nombre" class="col-form-label">Nombre:</label> <input type="text" class="form-control" id="rubroName2"> <label id="yaexiste' + dataName + '" style="color:red"></label></div>');
        $('#rubroName2').val(name);
        $('#editModal .modal-footer').html('<button type="button" class="btn btn-primary w-15" id="' + btnClass + '"><span class="spinner-border spinner-border-sm" role="status" aria-hidden="true" style="display: none;"></span><span class="btn-text">Aceptar</span></button><button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>');

        if (editType == 'Subrubro') {
            $('#editModal .modal-body').append('<label class="col-form-label">Rubro:</label><select id="rubroSelect" class="form-select" aria-label="Default select example" required> </select>');
            $.ajax({
                url: '/Carta/ObtenerRubros',
                type: 'GET',
                success: function (data) {
                    var select = $('#rubroSelect');
                    select.empty();
                    $.each(data, function (index, item) {
                        select.append($('<option></option>').val(item.valor).html(item.texto));
                    });
                    select.val(rubro);
                }
            });
        }

        $(document).on('click', '#' + btnClass, function () {
            var id = $('#editModal').data('id');
            var newName = $('#rubroName2').val();
            var btn = $(this);
            var data = {};
            data['Id' + editType] = id;
            data['Nombre'] = newName;

            if (editType == 'Subrubro') {
                data['IdRubro'] = $('#rubroSelect').val();
            }

            btn.find('.spinner-border').show();
            btn.find('.btn-text').hide();

            $.ajax({
                url: url,
                type: 'POST',
                data: data,
                success: function (response) {
                    if (response.success) {
                        var fila = $(tableName).find('tr[data-id="' + id + '"]');
                        fila.find('.nombre-' + dataName).text(newName);
                        $(tableName).dataTable().fnDestroy();
                        inicializarTabla(tableName);
                        var botonEditar = fila.find('.edit-btn-' + dataName);
                        botonEditar.data('name', newName);
                        if (editType == 'Subrubro') {
                            botonEditar.data('rubro', data['IdRubro']);
                            fila.find('.rubro-etiqueta').text(response.rubroNombre);
                        }
                        if (tableName == '#tblRubros' || tableName == '#tblSubRubros') {
                            $.ajax({
                                url: '/Carta/GetSubrubros',
                                method: 'GET',
                                success: function (data) {
                                    var table = $('#tblSubRubros').DataTable();
                                    table.clear();
                                    data.forEach(function (item) {
                                        table.row.add([
                                            item.idSubrubro,
                                            item.nombre,
                                            item.rubroNombre,
                                            '<div class="btn-group"><a class="btn btn-outline-primary btn-sm edit-btn-subrubro" data-id="' + item.idSubrubro + '" data-name="' + item.nombre + '" data-rubro="' + item.idRubro + '"><svg width="16" height="16"><use href="#icono-editar"/></svg></a><a class="btn btn-outline-danger btn-sm delete-btn-subrubro" data-id="' + item.idSubrubro + '" data-name="' + item.nombre + '"><svg width="16" height="16"><use href="#icono-eliminar"/></svg></a></div>'
                                        ]);
                                    });
                                    table.draw();
                                }
                            });
                        }

                        $('#editModal .modal-body').html('<div class="form-group text-center p-4"><svg width="29" height="29"><use href="#icono-confirmar" /></svg> Editado correctamente!</div>');
                        setTimeout(function () {
                            $('#editModal').modal('hide');
                            btn.find('.spinner-border').hide();
                            btn.find('.btn-text').show();
                            $('#editModal .modal-body').html('<div class="form-group"><label for="nombre" class="col-form-label">Nombre:</label><input type="text" class="form-control" id="rubroName2"></div>');
                        }, 1000);
                    }
                    else {
                        $('#yaexiste' + dataName).text(response.message);
                        btn.find('.spinner-border').hide();
                        btn.find('.btn-text').show();
                    }
                },
                error: function () {
                    alert("Error al editar " + editType + ".");
                }
            });
        });
    }

    $(document).on('click', '.edit-btn-rubro', function () {
        var id = $(this).data('id');
        var name = $(this).data('name');
        editModal('Rubro', id, name, '/Carta/EditarRubro', '#tblRubros', 'confirmEdit', 'rubro');
    });

    $(document).on('click', '.edit-btn-etiqueta', function () {
        var id = $(this).data('id');
        var name = $(this).data('name');
        editModal('Etiqueta', id, name, '/Carta/EditarEtiqueta', '#tblEtiquetas', 'confirmEdit2', 'etiqueta');
    });

    $(document).on('click', '.edit-btn-subrubro', function () {
        var id = $(this).data('id');
        var name = $(this).data('name');
        var rubro = $(this).data('rubro');
        editModal('Subrubro', id, name, '/Carta/EditarSubrubro', '#tblSubRubros', 'confirmEdit3', 'subrubro', rubro);
    });
});