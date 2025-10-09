import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import Login from './components/Login';
import StartSida from './components/StartSida';
import NyKund from './components/NyKund';
import BokaTid from './components/BokaTid';
import RegistreraJournal from './components/RegistreraJournal';
import BeställReservdel from './components/BeställReservdel';
import UppdateraKund from './components/UppdateraKund';
import UppdateraBokning from './components/UppdateraBokning';

const theme = createTheme({
  palette: {
    primary: {
      main: '#1976d2',
    },
    secondary: {
      main: '#dc004e',
    },
  },
});

function App() {
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Router>
        <Routes>
          <Route path="/" element={<Login />} />
          <Route path="/login" element={<Login />} />
          <Route path="/start" element={<StartSida />} />
          <Route path="/ny-kund" element={<NyKund />} />
          <Route path="/boka-tid" element={<BokaTid />} />
          <Route path="/registrera-journal" element={<RegistreraJournal />} />
          <Route path="/bestall-reservdel" element={<BeställReservdel />} />
          <Route path="/uppdatera-kund" element={<UppdateraKund />} />
          <Route path="/uppdatera-bokning" element={<UppdateraBokning />} />
        </Routes>
      </Router>
    </ThemeProvider>
  );
}

export default App;
