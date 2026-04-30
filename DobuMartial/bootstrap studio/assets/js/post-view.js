document.addEventListener('DOMContentLoaded', function() {
    const postBody = document.getElementById('post-body');
    const readMoreBtn = document.getElementById('read-more-btn');
    
    if (postBody && readMoreBtn) {
        const fullText = postBody.textContent;
        const limit = 1;

        if (fullText.length > limit) {
            const truncatedText = fullText.substring(0, limit) + '...';
            postBody.textContent = truncatedText;
            readMoreBtn.style.display = 'inline-block';

            readMoreBtn.addEventListener('click', function() {
                if (readMoreBtn.textContent === 'Read More') {
                    postBody.textContent = fullText;
                    readMoreBtn.textContent = 'Read Less';
                } else {
                    postBody.textContent = truncatedText;
                    readMoreBtn.textContent = 'Read More';
                }
            });
        }
    }

    // Comment editing functionality
    document.querySelectorAll('.edit-comment-btn').forEach(btn => {
        btn.addEventListener('click', function(e) {
            e.preventDefault();
            const commentCard = this.closest('.card-body');
            const commentTextElement = commentCard.querySelector('.comment-text');
            const actionsDiv = commentCard.querySelector('.edit-comment-actions');
            const originalText = commentTextElement.textContent;

            // Create textarea
            const textarea = document.createElement('textarea');
            textarea.className = 'form-control mb-2';
            textarea.value = originalText;
            textarea.rows = 3;

            // Switch view
            commentTextElement.classList.add('d-none');
            commentTextElement.parentNode.insertBefore(textarea, commentTextElement);
            actionsDiv.classList.remove('d-none');

            // Save button
            actionsDiv.querySelector('.save-edit-btn').onclick = function() {
                commentTextElement.textContent = textarea.value;
                cleanup();
            };

            // Cancel button
            actionsDiv.querySelector('.cancel-edit-btn').onclick = function() {
                cleanup();
            };

            function cleanup() {
                textarea.remove();
                commentTextElement.classList.remove('d-none');
                actionsDiv.classList.add('d-none');
            }
        });
    });
});