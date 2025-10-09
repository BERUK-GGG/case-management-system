import React, { useState, useEffect } from 'react';
import {
  Container,
  Paper,
  Typography,
  TextField,
  Button,
  Box,
  Alert,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  CircularProgress,
  Chip
} from '@mui/material';
import { useForm } from 'react-hook-form';
import { useNavigate } from 'react-router-dom';
import { partService } from '../services/api';

const BeställReservdel = () => {
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');
  const [loading, setLoading] = useState(false);
  const [existingParts, setExistingParts] = useState([]);
  const navigate = useNavigate();

  const { register, handleSubmit, watch, formState: { errors } } = useForm();

  const partType = watch('partType', 'new');

  useEffect(() => {
    const fetchParts = async () => {
      try {
        const response = await partService.getAll();
        setExistingParts(response.data);
      } catch (err) {
        setError('Kunde inte hämta befintliga reservdelar');
      }
    };
    fetchParts();
  }, []);

  const onSubmit = async (data) => {
    setLoading(true);
    setError('');
    setSuccess('');

    try {
      let partData;
      
      if (data.partType === 'existing') {
        // Update existing part quantity
        const existingPart = existingParts.find(p => p.id === parseInt(data.existingPartId));
        partData = {
          ...existingPart,
          quantity: existingPart.quantity + parseInt(data.quantity)
        };
        await partService.update(existingPart.id, partData);
        setSuccess(`Beställning av ${data.quantity} st ${existingPart.name} genomförd!`);
      } else {
        // Create new part
        partData = {
          name: data.name,
          partNumber: data.partNumber,
          price: parseFloat(data.price),
          quantity: parseInt(data.quantity),
          description: data.description,
          supplier: data.supplier || 'Ej specificerad'
        };
        await partService.create(partData);
        setSuccess(`Ny reservdel "${data.name}" beställd framgångsrikt!`);
      }
      
      setTimeout(() => {
        navigate('/');
      }, 2000);
    } catch (err) {
      setError(err.response?.data?.message || 'Ett fel uppstod vid beställning av reservdel');
    } finally {
      setLoading(false);
    }
  };

  const getStockStatus = (quantity) => {
    if (quantity === 0) return { color: 'error', label: 'Slut i lager' };
    if (quantity < 5) return { color: 'warning', label: 'Lågt lager' };
    return { color: 'success', label: 'I lager' };
  };

  return (
    <Container maxWidth="md" sx={{ mt: 4 }}>
      <Paper elevation={3} sx={{ p: 4 }}>
        <Typography variant="h4" component="h1" gutterBottom align="center">
          Beställ Reservdel
        </Typography>

        {error && <Alert severity="error" sx={{ mb: 2 }}>{error}</Alert>}
        {success && <Alert severity="success" sx={{ mb: 2 }}>{success}</Alert>}

        <Box component="form" onSubmit={handleSubmit(onSubmit)} sx={{ mt: 2 }}>
          <FormControl fullWidth margin="normal">
            <InputLabel>Typ av beställning</InputLabel>
            <Select
              {...register('partType')}
              label="Typ av beställning"
              defaultValue="new"
            >
              <MenuItem value="new">Ny reservdel</MenuItem>
              <MenuItem value="existing">Påfyllning befintlig reservdel</MenuItem>
            </Select>
          </FormControl>

          {partType === 'existing' && (
            <FormControl fullWidth margin="normal" error={!!errors.existingPartId}>
              <InputLabel>Befintlig reservdel</InputLabel>
              <Select
                {...register('existingPartId', { 
                  required: partType === 'existing' ? 'Reservdel måste väljas' : false 
                })}
                label="Befintlig reservdel"
                defaultValue=""
              >
                {existingParts.map((part) => {
                  const stockStatus = getStockStatus(part.quantity);
                  return (
                    <MenuItem key={part.id} value={part.id}>
                      <Box sx={{ display: 'flex', justifyContent: 'space-between', width: '100%', alignItems: 'center' }}>
                        <Box>
                          <Typography>{part.name}</Typography>
                          <Typography variant="caption" color="textSecondary">
                            {part.partNumber} - {part.price} kr
                          </Typography>
                        </Box>
                        <Box sx={{ display: 'flex', gap: 1, alignItems: 'center' }}>
                          <Typography variant="body2">{part.quantity} st</Typography>
                          <Chip 
                            label={stockStatus.label} 
                            color={stockStatus.color} 
                            size="small" 
                          />
                        </Box>
                      </Box>
                    </MenuItem>
                  );
                })}
              </Select>
              {errors.existingPartId && (
                <Typography variant="caption" color="error">
                  {errors.existingPartId.message}
                </Typography>
              )}
            </FormControl>
          )}

          {partType === 'new' && (
            <>
              <TextField
                fullWidth
                label="Namn"
                margin="normal"
                {...register('name', { 
                  required: partType === 'new' ? 'Namn är obligatoriskt' : false,
                  minLength: { value: 2, message: 'Namn måste vara minst 2 tecken' }
                })}
                error={!!errors.name}
                helperText={errors.name?.message}
              />

              <TextField
                fullWidth
                label="Artikelnummer"
                margin="normal"
                {...register('partNumber', { 
                  required: partType === 'new' ? 'Artikelnummer är obligatoriskt' : false
                })}
                error={!!errors.partNumber}
                helperText={errors.partNumber?.message}
              />

              <TextField
                fullWidth
                label="Pris (kr)"
                type="number"
                margin="normal"
                inputProps={{ min: 0, step: 0.01 }}
                {...register('price', { 
                  required: partType === 'new' ? 'Pris är obligatoriskt' : false,
                  min: { value: 0, message: 'Pris måste vara positivt' }
                })}
                error={!!errors.price}
                helperText={errors.price?.message}
              />

              <TextField
                fullWidth
                label="Leverantör"
                margin="normal"
                {...register('supplier')}
                helperText="Frivilligt - leverantör av reservdelen"
              />

              <TextField
                fullWidth
                label="Beskrivning"
                margin="normal"
                multiline
                rows={3}
                {...register('description')}
                helperText="Frivillig beskrivning av reservdelen"
              />
            </>
          )}

          <TextField
            fullWidth
            label="Antal"
            type="number"
            margin="normal"
            inputProps={{ min: 1 }}
            {...register('quantity', { 
              required: 'Antal är obligatoriskt',
              min: { value: 1, message: 'Antal måste vara minst 1' }
            })}
            error={!!errors.quantity}
            helperText={errors.quantity?.message}
          />

          <Box sx={{ mt: 3, display: 'flex', gap: 2 }}>
            <Button
              type="submit"
              variant="contained"
              color="primary"
              disabled={loading}
              sx={{ flex: 1 }}
            >
              {loading ? <CircularProgress size={24} /> : 'Beställ Reservdel'}
            </Button>
            <Button
              variant="outlined"
              onClick={() => navigate('/')}
              sx={{ flex: 1 }}
            >
              Avbryt
            </Button>
          </Box>
        </Box>
      </Paper>
    </Container>
  );
};

export default BeställReservdel;
