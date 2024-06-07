import { useState } from "react";
import reactLogo from "./assets/react.svg";
import viteLogo from "/vite.svg";
import "./App.css";
import CardList from "./Component/Card/CardList/CardList";
import Search from "./Component/Search/Search";

function App() {
  const [count, setCount] = useState(0);

  return (
    <div className="App">
      <Search />
      <CardList />
    </div>
  );
}

export default App;
