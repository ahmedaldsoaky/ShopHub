$(function () {

    $('#mytable').DataTable({

        processing: true,
        serverSide: true,
        responsive: false,
        autoWidth: false,

        pageLength: 10,
        lengthMenu: [10, 25, 50, 100],

        order: [[0, "asc"]],

        ajax: {
            url: "/Admin/User/GetData",
            type: "GET",

            data: function (d) {

                d.pageNumber = (d.start / d.length) + 1;
                d.pageSize = d.length;

                d.search = d.search.value;

                d.sortColumn = d.columns[d.order[0].column].data;
                d.sortDirection = d.order[0].dir;

            },

            dataSrc: function (json) {
                return json.data;
            }
        },

        columns: [
            {
                data: "fullName"
            },
            {
                data: "userName"
            },
            {
                data: "email"
            },
            {
                data: "phoneNumber"
            },
            {
                data: "role",
                className: "text-center",
                render: function (data) {

                    return data === "Admin"
                        ? `<span class="badge bg-primary">Admin</span>`
                        : `<span class="badge bg-success">Customer</span>`;
                }
            },
            {
                data: "isLocked",
                className: "text-center",
                render: function (data) {

                    return data
                        ? `<span class="badge bg-danger">Locked</span>`
                        : `<span class="badge bg-success">Active</span>`;
                }
            },
            {
                data: "id",
                orderable: false,
                searchable: false,
                className: "text-center",
                width: "180px",

                render: function (data, type, row) {

                    const roleButton = row.role === "Customer"
                                        ? `
                        <button class="btn btn-outline-primary btn-sm"
                                onclick="Promote('${data}')"
                                title="Promote">
                            <i class="fas fa-user-plus"></i>
                        </button>`
                                        : `
                        <button class="btn btn-outline-warning btn-sm"
                                onclick="Demote('${data}')"
                                title="Demote">
                            <i class="fas fa-user-minus"></i>
                        </button>`;

                    const lockButton = row.isLocked
                                        ? `
                        <button class="btn btn-outline-success btn-sm"
                                onclick="Unlock('${data}')"
                                title="Unlock">
                            <i class="fas fa-unlock"></i>
                        </button>`
                                        : `
                        <button class="btn btn-outline-secondary btn-sm"
                                onclick="Lock('${data}')"
                                title="Lock">
                            <i class="fas fa-lock"></i>
                        </button>`;

                    return `
                        <div class="btn-group">

                            ${roleButton}

                            ${lockButton}

                            <button
                                onclick="Delete('/User/Delete/${data}')"
                                class="btn btn-outline-danger btn-sm"
                                title="Delete">

                                <i class="fas fa-trash"></i>

                            </button>

                        </div>
                    `;
                }
            }
        ],

        language: {

            search: "_INPUT_",
            searchPlaceholder: "Search users...",

            lengthMenu: "Show _MENU_ users",

            info: "Showing _START_ to _END_ of _TOTAL_ users",

            infoEmpty: "No users found",

            zeroRecords: "No matching users found",

            processing: "Loading..."
        }

    });

}); 

function Promote(id) {
    handleUserAction(
        "Promote User?",
        "This user will become an Admin.",
        "question",
        "#0d6efd",
        "Yes, Promote",
        "/User/Promote",
        id
    );
}

function Demote(id) {
    handleUserAction(
        "Demote User?",
        "This user will become a Customer.",
        "warning",
        "#f0ad4e",
        "Yes, Demote",
        "/User/Demote",
        id
    );
}

function Lock(id) {
    handleUserAction(
        "Lock User?",
        "The user will not be able to login.",
        "warning",
        "#dc3545",
        "Yes, Lock",
        "/User/Lock",
        id
    );
}

function Unlock(id) {
    handleUserAction(
        "Unlock User?",
        "The user will be able to login again.",
        "question",
        "#198754",
        "Yes, Unlock",
        "/User/Unlock",
        id
    );
}

function handleUserAction(title, text, icon, color, confirmText, url, id) {

    Swal.fire({
        title,
        text,
        icon,
        showCancelButton: true,
        confirmButtonColor: color,
        cancelButtonColor: "#6c757d",
        confirmButtonText: confirmText
    }).then(result => {

        if (!result.isConfirmed) return;

        $.ajax({
            url: url,
            type: "POST",
            data: { id },

            success: function (response) {

                if (response.success) {
                    toastr.success(response.message);
                    $("#mytable").DataTable().ajax.reload(null, false);
                }
                else {
                    toastr.error(response.message);
                }
            },

            error: function () {
                toastr.error("Something went wrong.");
            }
        });

    });
}