﻿using FarmaciaLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmaciaLibrary.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly FarmaciaContext _context;

        public ClienteRepository(FarmaciaContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Cliente cliente)
        {
            if(cliente != null)
            {
                await _context.Clientes.AddAsync(cliente);
                return await _context.SaveChangesAsync() > 0;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var cli = await GetById(id);
            if(cli != null)
            {
                cli.Activo = false;
                return await _context.SaveChangesAsync() > 0;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Cliente>?> GetAll()
        {
            return await _context.Clientes.Where(c => c.Activo == true).ToListAsync();
        }

        public async Task<Cliente?> GetByDocumento(int documento)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Documento == documento && c.Activo == true);
        }

        public async Task<Cliente?> GetById(int id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.IdCliente == id && c.Activo == true);
        }

        public async Task<Cliente?> GetByName(string name)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Apellido == name);
        }

        public async Task<bool> Update(int id, Cliente cliente)
        {
            var cli = await _context.Clientes.FindAsync(id);
            if(cli != null)
            {
                cli.Nombre = cliente.Nombre;
                cli.Apellido = cliente.Apellido;
                cli.Activo = cliente?.Activo;
                cli.Documento = cliente?.Documento;
                cli.ObraSocialId = cliente?.ObraSocialId;

                return await _context.SaveChangesAsync() > 0;
            }
            else
            {
                return false;
            }
        }
    }
}
