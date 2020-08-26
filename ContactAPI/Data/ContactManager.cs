using ContactAPI.Repository;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ContactAPI.Data
{
    public class ContactManager : IDataRepository<Contact>
    {
        readonly ContactEntities _context;

        public ContactManager(ContactEntities context)
        {
            _context = context;
        }

        public void Add(Contact entity)
        {
            try
            {
                _context.Contacts.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Delete(Contact entity)
        {
            try
            {
                _context.Contacts.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Contact Get(long Id)
        {
            try
            {
                return _context.Contacts.FirstOrDefault(e => e.Id == Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<Contact> GetAll()
        {
            try {
                return _context.Contacts.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Contact dbEntity, Contact entity)
        {
            try {
                dbEntity.FirstName = entity.FirstName;
                dbEntity.LastName = entity.LastName;
                dbEntity.Email = entity.Email;
                dbEntity.PhoneNumber = entity.PhoneNumber;
                dbEntity.Status = entity.Status;

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
