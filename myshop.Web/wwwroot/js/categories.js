$(document).ready(() => {

    $('#mytable').DataTable({
        ajax: {
            url: '/Category/GetData',
            type: 'GET',
            dataSrc: 'data'
        },
        columns: [
            {
                data: 'name'
            },
            {
                data: 'description'
            },
            {
                data: 'createdTime',
                render: (data) => {
                    // Gracefully handle null or missing dates
                    return data ? new Date(data).toLocaleDateString() : 'N/A';
                }
            },
            {
                data: 'id',
                orderable: false,   // Disable sorting on the action column
                searchable: false,  // Disable searching on the action column
                render: (id) => {
                    return `
                        <div class="d-flex gap-2">
                            <a href="/Category/Edit/${id}" 
                               class="btn btn-success btn-sm action-btn" 
                               title="Edit Category">
                                <i class="fa-solid fa-pen"></i>
                            </a>

                            <button onclick="Delete('/Category/Delete/${id}')"
                                    class="btn btn-danger btn-sm action-btn" 
                                    title="Delete Category">
                                <i class="fa-solid fa-trash"></i>
                            </button>
                        </div>
                    `;
                }
            }
        ]
    });

});