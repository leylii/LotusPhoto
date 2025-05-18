function Header() {
    return (
      <header className="flex items-center justify-between px-6 py-4 shadow-md">
        <div className="flex items-center gap-2">
          <img src="/src/assets/lotus.png" alt="Logo" className="h-8 w-auto" />
          <span className="text-xl font-bold text-gray-800">Lotus Gallery</span>
        </div>
      </header>
    );
  }
  
  export default Header;  