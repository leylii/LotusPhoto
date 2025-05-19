# LotusPhoto

This is a personal photo gallery project I'm currently building.  
The goal is to create a clean and simple platform where I can display my photos, categorize them, and eventually offer them for sale.

The project is still under development, but the core features are already working.

## What it does (so far)

- Displays a collection of photos with categories like "Landscape", "Portrait", etc.
- Responsive frontend built with React and TailwindCSS
- Backend built with ASP.NET Core Web API
- Each photo has a detail page (title, category, price)

## How to run it locally

### Backend (ASP.NET Core)
1. Navigate to the `backend` folder
2. Open the solution in Visual Studio (`LotusPhoto.sln`)
3. Run the `LotusPhoto.Api` project

Note: By default, the API will run on `https://localhost:xxxx`.


### Frontend (React + Vite)
To run the frontend locally using Visual Studio Code:

1. Open the folder: frontend/LotusPhoto.Web in VS Code
2. Open Terminal from Terminal => New Terminal
3. Run: npm install
4. Run: npm run dev
 
Make sure to configure the correct API base URL in .env file like:
VITE_API_BASE_URL=https://localhost:xxxx (replace with your backend port)



Make sure the backend is running before using the frontend

## ScreenShots
Here’s how the gallery looks:

![LotusPhoto](https://github.com/user-attachments/assets/284406df-d291-469b-bcb9-681bc25d08b5)

![LotusPhoto01](https://github.com/user-attachments/assets/9a4ae7c5-8f61-4ebf-a4aa-af65939151cb)

![LotusPhoto03](https://github.com/user-attachments/assets/200f8516-49f3-4318-993d-dbedb7bf3d30)

![LotusPhoto02](https://github.com/user-attachments/assets/7e333dad-2a66-4ae4-a541-4ab337a50f49)

This project is being developed gradually. 
Please refer to [TODO.md](./TODO.md) for future plans and ideas.
