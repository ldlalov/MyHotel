$(document).ready(function () {
    $('.delete').click(function () {
        var id = $(this).data('id');
        var deleteUrl = $(this).data('delete-url'); // Get the deletion URL from the data attribute

        $('#confirmDeleteModal').modal('show');

        // Handler for "Yes" button click
        $('#confirmDeleteBtn').off('click').on('click', function () {
            // Proceed with the deletion using the retrieved URL
            window.location.href = deleteUrl;
        });

        // Handler for modal close event
        $('#confirmDeleteModal').on('hidden.bs.modal', function () {
            // Remove the click event handler for the "Yes" button
            $('#confirmDeleteBtn').off('click');
        });

        // Handler for "No" button click
        $('#cancelDeleteBtn').off('click').on('click', function () {
            $('#confirmDeleteModal').modal('hide');
        });
    });
});
