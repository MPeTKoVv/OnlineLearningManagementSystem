document.addEventListener("DOMContentLoaded", function () {
    // Success notification
    if (window.TempDataSuccessMessage) {
        Swal.fire({
            icon: 'success',
            title: 'Success',
            text: window.TempDataSuccessMessage,
            timer: 2000,
            showConfirmButton: false
        });
    }

    // Error notification
    if (window.TempDataErrorMessage) {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: window.TempDataErrorMessage,
            timer: 2000,
            showConfirmButton: false
        });
    }
});
