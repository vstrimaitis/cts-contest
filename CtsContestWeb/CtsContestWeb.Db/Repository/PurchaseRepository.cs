﻿using System;
using System.Collections.Generic;
using CtsContestWeb.Db.DataAccess;
using CtsContestWeb.Db.Entities;
using CtsContestWeb.Dto;

namespace CtsContestWeb.Db.Repository
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public PurchaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Purchase purchase)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Purchase> GetAllByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public bool GiveAway(Guid id)
        {
            _dbContext.GivenPurchases.Add(new GivenPurchase
            {
                Created = DateTime.Now,
                GivenPurchaseId = id
            });
            return _dbContext.SaveChanges() == 1;
        }

        public PurchaseDto GetPurchaseByPurchaseGuid(Guid id)
        {
            var purchase = _dbContext.Purchases.Find(id);
            purchase.GivenPurchase = _dbContext.GivenPurchases.Find(id);

            return new PurchaseDto
            {
                PurchaseId = purchase.PurchaseId,
                UserEmail = purchase.UserEmail,
                PrizeId = purchase.PrizeId,
                IsGivenAway = purchase.GivenPurchase != null
            };
        }
    }
}
