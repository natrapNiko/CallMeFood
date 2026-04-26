# 🍔 CallMeFood

**CallMeFood** is a full-stack recipe-sharing web application built as a defence project. It allows users to discover, create, and share recipes organised by category, with role-based access control for regular users and administrators.

---

## 📸 Screenshots

### Home Page
![CallMeFood Home Page](screenshots/screenshot1.png)

### Recipes by Category — Starters
![Recipes in Starter category](screenshots/screenshot2.png)

### Recipes by Category — Main Dishes
![Recipes in Main Dishes category](screenshots/screenshot3.png)

---

## ✨ Features

- **Browse Recipes** — Explore recipes organised by category (Starters, Main Dishes, etc.)
- **Search** — Find recipes quickly via the search bar
- **Share a Recipe** — Authenticated users can submit their own recipes
- **Category Filtering** — Navigate recipes by category via the Categories menu
- **User Authentication** — Register and log in to unlock full functionality
- **Role-Based Access** — Admins have full content management capabilities
- **Multilingual Content** — Supports recipes in multiple languages (e.g., Bulgarian, English)
- **Comments** — Logged-in users can comment on recipes
- **Contact** — Users can reach the platform team via the Contact Us link

---

## 🛠️ Tech Stack

> Update this section to match your actual implementation.

| Layer | Technology |
|---|---|
| Frontend | HTML, CSS, JavaScript |
| Backend | (e.g., Django / Flask / Node.js) |
| Database | (e.g., PostgreSQL / SQLite / MongoDB) |
| Auth | Session-based / JWT |
| Deployment | (e.g., Heroku / Railway / VPS) |

---

## 🚀 Getting Started

### Prerequisites

- Python 3.x / Node.js (depending on your stack)
- pip / npm
- A running database instance

### Installation

```bash
# Clone the repository
git clone https://github.com/natrapNiko/CallMeFood.git
cd CallMeFood

# Install dependencies
pip install -r requirements.txt   # or: npm install

# Apply database migrations
python manage.py migrate          # Django example

# Run the development server
python manage.py runserver        # or: npm start
```

Open your browser at `http://localhost:8000`.

---

## 📁 Project Structure

```
CallMeFood/
├── screenshots/          # Screenshots for README
├── static/               # CSS, JS, images
├── templates/            # HTML templates
├── recipes/              # Core recipe app / module
├── users/                # Authentication & user management
├── requirements.txt      # Python dependencies
└── README.md
```

---

## 👤 Author

**Nikolay Natrap**
GitHub: [@natrapNiko](https://github.com/natrapNiko)

---

## 📄 Licence

This project was developed for academic/defence purposes.

---

*© 2025 CallMeFood*
