import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import {
  Container,
  Typography,
  Grid,
  Card,
  CardContent,
  CardActions,
  Button,
  Box,
} from '@mui/material';
import {
  PersonAdd,
  Event,
  NoteAdd,
  Build,
  PersonOutline,
  EventAvailable,
} from '@mui/icons-material';
import Header from './Header';




const StartSida = () => {
  const navigate = useNavigate();

  useEffect(() => {
    // Check if user is logged in
    const isLoggedIn = localStorage.getItem('isLoggedIn');
    if (!isLoggedIn) {
      navigate('/login');
    }
  }, [navigate]);

  const menuItems = [
    {
      title: 'Ny Kund',
      description: 'Registrera en ny kund i systemet',
      icon: <PersonAdd fontSize="large" />,
      path: '/ny-kund',
      color: 'primary',
    },
    {
      title: 'Boka Tid',
      description: 'Boka ett nytt besök för en kund',
      icon: <Event fontSize="large" />,
      path: '/boka-tid',
      color: 'secondary',
    },
    {
      title: 'Registrera Journal',
      description: 'Registrera utförd service och reparationer',
      icon: <NoteAdd fontSize="large" />,
      path: '/registrera-journal',
      color: 'success',
    },
    {
      title: 'Beställ Reservdel',
      description: 'Lägg till nya reservdelar i systemet',
      icon: <Build fontSize="large" />,
      path: '/bestall-reservdel',
      color: 'warning',
    },
    {
      title: 'Uppdatera Kund',
      description: 'Uppdatera befintlig kundinformation',
      icon: <PersonOutline fontSize="large" />,
      path: '/uppdatera-kund',
      color: 'info',
    },
    {
      title: 'Uppdatera Bokning',
      description: 'Ändra befintliga bokningar och besök',
      icon: <EventAvailable fontSize="large" />,
      path: '/uppdatera-bokning',
      color: 'error',
    },
  ];

  return (
    <div className="app-container">
      <Header />

      <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
        <Typography variant="h4" gutterBottom align="center">
          Välkommen till RB Ärendesystem
        </Typography>
        <Typography variant="subtitle1" gutterBottom align="center" color="textSecondary">
          Välj en funktion nedan för att komma igång
        </Typography>

        <Grid container spacing={3} sx={{ mt: 2 }}>
          {menuItems.map((item, index) => (
            <Grid item xs={12} sm={6} md={4} key={index}>
              <Card
                sx={{
                  height: '100%',
                  display: 'flex',
                  flexDirection: 'column',
                  transition: 'transform 0.2s, box-shadow 0.2s',
                  '&:hover': {
                    transform: 'translateY(-4px)',
                    boxShadow: 4,
                  },
                }}
              >
                <CardContent sx={{ flexGrow: 1, textAlign: 'center' }}>
                  <Box sx={{ color: `${item.color}.main`, mb: 2 }}>
                    {item.icon}
                  </Box>
                  <Typography gutterBottom variant="h6" component="h2">
                    {item.title}
                  </Typography>
                  <Typography variant="body2" color="textSecondary">
                    {item.description}
                  </Typography>
                </CardContent>
                <CardActions sx={{ justifyContent: 'center', pb: 2 }}>
                  <Button
                    size="medium"
                    variant="contained"
                    color={item.color}
                    onClick={() => navigate(item.path)}
                  >
                    Öppna
                  </Button>
                </CardActions>
              </Card>
            </Grid>
          ))}
        </Grid>
        
        
      </Container>
    
    </div>
  );
};

export default StartSida;
