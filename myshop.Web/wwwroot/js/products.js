$(document).ready(() => {

    $('#mytable').DataTable({
        ajax: {
            url: '/Product/GetData',
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
                data: 'price',
                render: (data) => {
                    // Gracefully format price to 2 decimal places with a dollar sign
                    return data != null ? `$${parseFloat(data).toFixed(2)}` : 'N/A';
                }
            },
            {
                data: 'categoryName'
            },
            {
                data: 'id',
                orderable: false,   // Disable sorting on the action column
                searchable: false,  // Disable searching on the action column
                render: (id) => {
                    return `
                        <div class="d-flex gap-2">
                            <a href="/Product/Edit/${id}" 
                               class="btn btn-success btn-sm action-btn"
                               title="Edit Product">
                                <i class="fa-solid fa-pen"></i>
                            </a>

                            <button onclick="Delete('/Product/Delete/${id}')"
                                    class="btn btn-danger btn-sm action-btn"
                                    title="Delete Product">
                                <i class="fa-solid fa-trash"></i>
                            </button>
                        </div>
                    `;
                }
            }
        ]
    });

});