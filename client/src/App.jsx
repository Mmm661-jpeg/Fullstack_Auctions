import { useEffect, useState } from 'react'
import { Routes, Route } from "react-router-dom";





import MainPage1 from './pages/MainPage1';
import ItemPage1 from './pages/ItemPage1';
import UserPage1 from './pages/UserPage1';
import Login_Register_Page from './pages/Login_Register_Page/Login_Register_Page';



function App() {
  
 
  return (
    <>
    <Routes>

      <Route path="/" element={<MainPage1/>}/>
      <Route path="/loginpage" element={<Login_Register_Page/>}/>
      <Route path="/itempage/:id" element={<ItemPage1/>}/>
      <Route path="/userpage" element={<UserPage1/>}/>
      
      
    </Routes>
    
    </>
      
  )
}

export default App
