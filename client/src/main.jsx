import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter as Router } from "react-router-dom";

import App from './App.jsx'
import UserProvider from './context/UserContext/UserProvider.Jsx'
import AuctionProvider from './context/AuctionContext/AuctionProvider.Jsx';


createRoot(document.getElementById('root')).render(
  <StrictMode>
    <UserProvider>
      <AuctionProvider>

        <Router>
          <App />
        </Router>

      </AuctionProvider>
    </UserProvider>

   
   
  </StrictMode>,
)
