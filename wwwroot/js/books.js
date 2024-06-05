function confirmDelete(bookId) {
    if (confirm("Are you sure you want to delete this book?")) {
        window.location.href = "/Books/" + bookId + "/Delete"
    }
}