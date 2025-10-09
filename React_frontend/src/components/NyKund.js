import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import {
  Container,
  Paper,
  TextField,
  Button,
  Typography,
  Box,
  Alert,
  AppBar,
  Toolbar,
  IconButton,
} from '@mui/material';
import { ArrowBack } from '@mui/icons-material';
import { useForm } from 'react-hook-form';
import { customerService } from '../services/api';

const NyKund = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [success, setSuccess] = useState(false);
  const [error, setError] = useState('');
  
  const { register, handleSubmit, formState: { errors }, reset } = useForm();

  const onSubmit = async (data) => {
    setLoading(true);
    setError('');
    
    try {
      const response = await customerService.create({
        namn: data.namn,
        personNr: parseInt(data.personNr),
        address: data.address,
        teleNr: parseInt(data.teleNr),
        epost: data.epost,
      });
      
      console.log('Kund skapad:', response.data);
      setSuccess(true);
      reset();
      
      // Auto-hide success message after 3 seconds
      setTimeout(() => setSuccess(false), 3000);
    } catch (err) {
      console.error('Fel vid skapande av kund:', err);
      setError('Kunde inte skapa kunden. Försök igen.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="app-container">
      <AppBar position="static">
        <Toolbar>
          <IconButton
            edge="start"
            color="inherit"
            onClick={() => navigate('/start')}
            sx={{ mr: 2 }}
          >
            <ArrowBack />
          </IconButton>
          <Typography variant="h6" component="div">
            Ny Kund
          </Typography>
        </Toolbar>
      </AppBar>

      <Container maxWidth="md" sx={{ mt: 4, mb: 4 }}>
        <Paper elevation={3} sx={{ p: 4 }}>
          <Typography variant="h4" gutterBottom align="center">
            Registrera Ny Kund
          </Typography>
          
          {success && (
            <Alert severity="success" sx={{ mb: 2 }}>
              Kunden har registrerats framgångsrikt!
            </Alert>
          )}
          
          {error && (
            <Alert severity="error" sx={{ mb: 2 }}>
              {error}
            </Alert>
          )}

          <Box component="form" onSubmit={handleSubmit(onSubmit)} sx={{ mt: 2 }}>
            <TextField
              margin="normal"
              required
              fullWidth
              id="namn"
              label="Namn"
              name="namn"
              autoFocus
              {...register('namn', { 
                required: 'Namn krävs',
                minLength: { value: 2, message: 'Namn måste vara minst 2 tecken' }
              })}
              error={!!errors.namn}
              helperText={errors.namn?.message}
            />

            <TextField
              margin="normal"
              required
              fullWidth
              id="personNr"
              label="Personnummer"
              name="personNr"
              type="number"
              {...register('personNr', { 
                required: 'Personnummer krävs',
                pattern: {
                  value: /^\d+$/,
                  message: 'Personnummer får endast innehålla siffror'
                }
              })}
              error={!!errors.personNr}
              helperText={errors.personNr?.message}
            />

            <TextField
              margin="normal"
              required
              fullWidth
              id="address"
              label="Adress"
              name="address"
              {...register('address', { 
                required: 'Adress krävs',
                minLength: { value: 5, message: 'Adress måste vara minst 5 tecken' }
              })}
              error={!!errors.address}
              helperText={errors.address?.message}
            />

            <TextField
              margin="normal"
              required
              fullWidth
              id="teleNr"
              label="Telefonnummer"
              name="teleNr"
              type="number"
              {...register('teleNr', { 
                required: 'Telefonnummer krävs',
                pattern: {
                  value: /^\d+$/,
                  message: 'Telefonnummer får endast innehålla siffror'
                }
              })}
              error={!!errors.teleNr}
              helperText={errors.teleNr?.message}
            />

            <TextField
              margin="normal"
              required
              fullWidth
              id="epost"
              label="E-post"
              name="epost"
              type="email"
              {...register('epost', { 
                required: 'E-post krävs',
                pattern: {
                  value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                  message: 'Ogiltig e-postadress'
                }
              })}
              error={!!errors.epost}
              helperText={errors.epost?.message}
            />

            <Box sx={{ mt: 3, display: 'flex', gap: 2 }}>
              <Button
                type="submit"
                variant="contained"
                disabled={loading}
                sx={{ flex: 1 }}
              >
                {loading ? 'Sparar...' : 'Spara Kund'}
              </Button>
              <Button
                variant="outlined"
                onClick={() => reset()}
                disabled={loading}
                sx={{ flex: 1 }}
              >
                Rensa
              </Button>
            </Box>
          </Box>
        </Paper>
      </Container>
    </div>
  );
};

export default NyKund;
