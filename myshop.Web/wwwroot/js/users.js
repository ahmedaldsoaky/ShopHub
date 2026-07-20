$(document).ready(() => {
    loadData();
});

const loadData = () => {
    $('#mytable').DataTable({
        destroy: true,
        ajax: {
            url: '/User/GetData',
            type: 'GET',
            dataSrc: 'data'
        },
        columns: [
            {
                data: 'fullName'
            },
            {
                data: 'userName'
            },
            {
                data: 'email'
            },
            {
                data: 'role',
                render: (data) => {
                    return data === 'Admin'
                        ? `<span class="badge bg-primary">Admin</span>`
                        : `<span class="badge bg-success">Customer</span>`;
                }
            },
            {
                data: 'isLocked',
                render: (data) => {
                    return data
                        ? `<span class="badge bg-danger">Locked</span>`
                        : `<span class="badge bg-success">Active</span>`;
                }
            },
            {
                data: 'id',
                orderable: false,
                searchable: false,
                render: (data, type, row) => {

                    // Determine Role Button
                    const roleButton = row.role === 'Customer'
                        ? `<button class="btn btn-primary btn-sm action-btn" onclick="Promote('${data}')" title="Promote to Admin">
                               <i class="fa-solid fa-user-shield"></i>
                           </button>`
                        : `<button class="btn btn-warning btn-sm action-btn" onclick="Demote('${data}')" title="Demote to Customer">
                               <i class="fa-solid fa-user"></i>
                           </button>`;

                    // Determine Lock Button
                    const lockButton = row.isLocked
                        ? `<button class="btn btn-success btn-sm action-btn" onclick="Unlock('${data}')" title="Unlock User">
                               <i class="fa-solid fa-lock-open"></i>
                           </button>`
                        : `<button class="btn btn-secondary btn-sm action-btn" onclick="Lock('${data}')" title="Lock User">
                               <i class="fa-solid fa-lock"></i>
                           </button>`;

                    // Return combined buttons
                    return `
                        <div class="d-flex gap-2">
                            ${roleButton}
                            ${lockButton}
                            <button class="btn btn-danger btn-sm action-btn" onclick="Delete('/User/Delete/${data}')" title="Delete User">
                                <i class="fa-solid fa-trash"></i>
                            </button>
                        </div>
                    `;
                }
            }
        ]
    });
};

// ==========================================
// Reusable Helper for SweetAlert & AJAX
// ==========================================
const handleUserAction = (title, text, icon, confirmColor, confirmText, url, id) => {
    Swal.fire({
        title: title,
        text: text,
        icon: icon,
        showCancelButton: true,
        confirmButtonColor: confirmColor,
        cancelButtonColor: '#6c757d',
        confirmButtonText: confirmText
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'POST',
                data: { id: id },
                success: (response) => {
                    if (response.success) {
                        toastr.success(response.message);
                        $('#mytable').DataTable().ajax.reload();
                    } else {
                        toastr.error(response.message);
                    }
                },
                error: () => {
                    toastr.error("An error occurred while communicating with the server.");
                }
            });
        }
    });
};

// ==========================================
// Action Triggers
// ==========================================
const Promote = (id) => {
    handleUserAction('Promote User?', 'This user will become an Admin.', 'question', '#0d6efd', 'Yes, Promote', '/User/Promote', id);
};

const Demote = (id) => {
    handleUserAction('Demote User?', 'This user will become a Customer.', 'warning', '#f0ad4e', 'Yes, Demote', '/User/Demote', id);
};

const Lock = (id) => {
    handleUserAction('Lock User?', 'The user will not be able to login.', 'warning', '#dc3545', 'Yes, Lock', '/User/Lock', id);
};

const Unlock = (id) => {
    handleUserAction('Unlock User?', 'The user will be able to login again.', 'question', '#198754', 'Yes, Unlock', '/User/Unlock', id);
};