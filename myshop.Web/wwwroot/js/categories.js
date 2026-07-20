$(document).ready(function () {

    $("#mytable").DataTable({

        ajax: {
            url: "/Category/GetData",
            type: "GET",
            dataSrc: "data"
        },

        columns: [
            { data: "name" },
            { data: "description" },
            {
                data: "createdTime",
                render: function (data) {
                    return new Date(data).toLocaleDateString();
                }
            },
            {
                data: "id",
                render: function (id) {
                    return `
                        <a href="/Category/Edit/${id}" class="btn btn-success btn-sm">
                            <i class="fa-solid fa-pen"></i>
                        </a>

                        <button onclick="Delete('/Category/Delete/${id}')"
                                class="btn btn-danger btn-sm">
                            <i class="fa-solid fa-trash"></i>
                        </button>
                    `;
                }
            }
        ]
    });

});

function Delete(url) {

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to undo this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {

        if (result.isConfirmed) {

            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {

                    if (data.success) {
                        $("#mytable").DataTable().ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });

        }

    });

}