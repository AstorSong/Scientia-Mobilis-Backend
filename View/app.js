const API_URL = "http://localhost:5184/api/Upload";

document.getElementById("uploadForm").addEventListener("submit", async (e) => {
  e.preventDefault();
  const book = {
    title: document.getElementById("title").value,
    author: document.getElementById("author").value,
    fileUrl: document.getElementById("fileUrl").value
  };
  await fetch(API_URL, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(book)
  });
  loadBooks();
  e.target.reset();
});

async function loadBooks() {
  const res = await fetch(API_URL);
  const books = await res.json();
  const container = document.getElementById("bookList");
  container.innerHTML = "";

  books.forEach(book => {
    const card = document.createElement("div");
    card.className = "card book-card";
    card.innerHTML = `
      <div class="card-body">
        <h5 class="card-title editable-title">${book.title}</h5>
        <input class="form-control d-none edit-title" value="${book.title}">
        
        <p class="card-text editable-author mb-1"><strong>Author:</strong> ${book.author}</p>
        <input class="form-control d-none edit-author" value="${book.author}">
        
        <p class="card-text editable-url"><strong>URL:</strong> <a href="${book.fileUrl}" target="_blank">${book.fileUrl}</a></p>
        <input class="form-control d-none edit-url" value="${book.fileUrl}">

        <div class="mt-3 text-end">
          <button class="btn btn-sm btn-outline-primary edit-btn">Edit</button>
          <button class="btn btn-sm btn-success d-none save-btn">Save</button>
          <button class="btn btn-sm btn-danger delete-btn">Delete</button>
        </div>
      </div>
    `;
    container.appendChild(card);

    // 绑定按钮行为
    const editBtn = card.querySelector(".edit-btn");
    const saveBtn = card.querySelector(".save-btn");
    const deleteBtn = card.querySelector(".delete-btn");

    editBtn.addEventListener("click", () => {
      toggleEditMode(card, true);
    });

    saveBtn.addEventListener("click", async () => {
      const updated = {
        title: card.querySelector(".edit-title").value,
        author: card.querySelector(".edit-author").value,
        fileUrl: card.querySelector(".edit-url").value
      };

      await fetch(`${API_URL}/${book.id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(updated)
      });
      loadBooks();
    });

    deleteBtn.addEventListener("click", async () => {
      if (confirm("Delete this book?")) {
        await fetch(`${API_URL}/${book.id}`, { method: "DELETE" });
        loadBooks();
      }
    });
  });
}

function toggleEditMode(card, editing) {
  card.querySelector(".editable-title").classList.toggle("d-none", editing);
  card.querySelector(".editable-author").classList.toggle("d-none", editing);
  card.querySelector(".editable-url").classList.toggle("d-none", editing);

  card.querySelector(".edit-title").classList.toggle("d-none", !editing);
  card.querySelector(".edit-author").classList.toggle("d-none", !editing);
  card.querySelector(".edit-url").classList.toggle("d-none", !editing);

  card.querySelector(".edit-btn").classList.toggle("d-none", editing);
  card.querySelector(".save-btn").classList.toggle("d-none", !editing);
}

loadBooks();
