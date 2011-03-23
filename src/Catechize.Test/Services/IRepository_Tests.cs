using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Catechize.Services;

namespace Catechize.Test.Services
{
    public class TestObject {
        public int ID {get; set;}
        public string Value {get; set;}
    }

    public class TestRepo : IRepository<TestObject>{

        public IList<TestObject> items = new List<TestObject>();

        public TestRepo() {
            items.Add(new TestObject() {ID = 1, Value="item1" });
            items.Add(new TestObject() {ID = 2, Value="item2" });
        }


        public TestObject GetByID(int ID)
        {
            if (0 >= ID)
                throw new ArgumentOutOfRangeException("ID", "ID must have a value of 1 (one) or greater");

            foreach (TestObject obj in items)
                if (ID == obj.ID)
                    return obj;

            return null;
        }

        public void Create(TestObject value)
        {
            if (null == value)
                throw new ArgumentNullException("value");
            if (value.ID != 0)
                throw new ArgumentException("TestObject.ID cannot be set before passing to Create. Create should set the ID after the object is created", "value");

            int newID = (from item in GetAll()
                         select item.ID).Max() + 1;

            value.ID = newID;

            items.Add(value);
        }

        public void Update(TestObject value)
        {
            if (null == value)
                throw new ArgumentNullException("value");

            if (value.ID <= 0)
                throw new ArgumentException("value's ID property must have a value of 1 (one) or greater", "value");

            TestObject foundItem = null; 
            foreach (TestObject obj in this.items) {
                if (obj.ID == value.ID)
                    foundItem = obj;
            }

            if (null == foundItem)
                throw new ArgumentException("value's ID property contains an identifier that doesn't exist in the data source");

            items.Remove(foundItem);
            items.Add(value);
        }

        public void Delete(TestObject value)
        {
            if (null == value)
                throw new ArgumentNullException("value");

            if (value.ID <= 0)
                throw new ArgumentException("value's ID property must have a value of 1 (one) or greater", "value");

            TestObject foundItem = null;
            foreach (TestObject obj in this.items)
            {
                if (obj.ID == value.ID)
                    foundItem = obj;
            }

            if (null == foundItem)
                throw new ArgumentException("value's ID property contains an identifier that doesn't exist in the data source");

            items.Remove(foundItem);
        }

        public void Delete(int ID)
        {
            if (ID <= 0)
                throw new ArgumentException("ID must have a value of 1 (one) or greater", "value");

            TestObject foundItem = null;
            foreach (TestObject obj in this.items)
            {
                if (obj.ID == ID)
                    foundItem = obj;
            }

            if (null == foundItem)
                throw new ArgumentException("value's ID property contains an identifier that doesn't exist in the data source");

            items.Remove(foundItem);
        }

        public IQueryable<TestObject> GetAll()
        {
            return items.AsQueryable<TestObject>();
        }
    }

    public class IRepository_Tests
    {
        public IRepository<TestObject> GetService()
        {
            return new TestRepo();
        }


        //TEST:GetByID

        [Fact]
        public void GetByID_PassExistingItemID_ReturnsNonNullObject() {
            IRepository<TestObject> service = GetService();

            int existingID = 1;
            Assert.NotNull(service.GetByID(existingID));
        }

        [Fact]
        public void GetByID_PassZeroID_ThrowsException()
        {
            IRepository<TestObject> service = GetService();

            int invalidID = 0;
            Assert.Throws<ArgumentOutOfRangeException>(() => service.GetByID(invalidID));
        }

        [Fact]
        public void GetByID_PassNonExistingItemID_ReturnsNullObject()
        {
            IRepository<TestObject> service = GetService();

            int nonExistingID = 200;
            Assert.Null(service.GetByID(nonExistingID));
        }

        [Fact]
        public void GetByID_PassExistingItemID_ReturnsObjectWithTheCorrectID()
        {
            IRepository<TestObject> service = GetService();
            int existingID = 1;
            Assert.Equal(service.GetByID(existingID).ID, existingID);
        }

        [Fact]
        public void GetByID_PassExistingItemID_ReturnsObjectWithTheCorrectID2()
        {
            IRepository<TestObject> service = GetService();
            int existingID = 2;
            Assert.Equal(service.GetByID(existingID).ID, existingID);
        }


        //TEST:Create

        [Fact]
        public void Create_PassNullObject_ThrowsException()
        {
            IRepository<TestObject> service = GetService();
            Assert.Throws<ArgumentNullException>(() => service.Create(null));
        }

        [Fact]
        public void Create_PassObjectWithIDSetID_ThrowsException()
        {
            IRepository<TestObject> service = GetService();
            TestObject objectWithIDSet = new TestObject() { ID = 500 };

            Assert.Throws<ArgumentException>(() => service.Create(objectWithIDSet));
        }

        [Fact]
        public void Create_PassValidObject_ObjectIDIsSet()
        {
            IRepository<TestObject> service = GetService();
            TestObject newTestObject = new TestObject() { Value = "MISC TEXT" };
            int nextAvailableID = (from item in service.GetAll()
                                   select item.ID).Max() + 1;
            service.Create(newTestObject);

            Assert.Equal( nextAvailableID, newTestObject.ID);
        }


        //TEST:Update

        [Fact]
        public void Update_PassNullObject_ThrowsException()
        {
            IRepository<TestObject> service = GetService();
            Assert.Throws<ArgumentNullException>(() => service.Update(null));
        }

        [Fact]
        public void Update_PassObjectWithInvalidID_ThrowsException()
        {
            // ID is less than or equal to zero
            IRepository<TestObject> service = GetService();

            TestObject withInvalidID = new TestObject() {
                ID = 0
            };

            Assert.Throws<ArgumentException>(() => service.Update(withInvalidID));
        }

        [Fact]
        public void Update_PassObjectNonExistingID_ThrowsException()
        {
            // Item with ID doesn't exist in the data source
            IRepository<TestObject> service = GetService();

            int idNotExistingInDataSource = 9999999;
            TestObject withNonExistingID = new TestObject() {
                ID = idNotExistingInDataSource
            };

            Assert.Throws<ArgumentException>( () => service.Update(withNonExistingID) );
        }

        [Fact]
        public void Update_PassObjectExistingIDAndAlteredData_ItemsValueIsUpdated()
        {
            // Item with ID doesn't exist in the data source
            IRepository<TestObject> service = GetService();

            int idExistingInDataSource = 2;
            string oldValue = "item2";
            string newValue = "new value";

            TestObject withNonExistingID = new TestObject()
            {
                ID = idExistingInDataSource,
                Value = "new value"
            };

            service.Update(withNonExistingID);

            Assert.Equal(newValue, service.GetByID(idExistingInDataSource).Value);
        }


        // TEST:Delete()

        [Fact]
        public void Delete_PassNull_ThrowsException()
        {
            IRepository<TestObject> service = GetService();

            Assert.Throws<ArgumentNullException>(() => service.Delete(null));
        }

        [Fact]
        public void Delete_PassObjectWithInvalidID_ThrowsException()
        {
            // ID is less than or equal to zero
            IRepository<TestObject> service = GetService();

            TestObject withInvalidID = new TestObject()
            {
                ID = 0
            };

            Assert.Throws<ArgumentException>(() => service.Delete(withInvalidID));
        }

        [Fact]
        public void Delete_PassObjectWithIDThatDoesntExist_ThrowsException()
        {
            IRepository<TestObject> service = GetService();

            int idThatDoesntExistInDataSource = 999999;
            TestObject withNonExistingID = new TestObject() {
                ID = idThatDoesntExistInDataSource
            };

            Assert.Throws<ArgumentException>(() => service.Delete(withNonExistingID));
        }


        [Fact]
        public void Delete_PassObjectWithIDThatDoesExist_ItemDeleted()
        {
            IRepository<TestObject> service = GetService();
            int idThatExists = 2;
            TestObject withExistingID = new TestObject()
            {
                ID = idThatExists
            };

            service.Delete(withExistingID);
            Assert.Null(service.GetByID(idThatExists));
        }



        [Fact]
        public void DeleteWithID_PassObjectWithInvalidID_ThrowsException()
        {
            // ID is less than or equal to zero
            IRepository<TestObject> service = GetService();
            int withInvalidID = 0;
            Assert.Throws<ArgumentException>(() => service.Delete(withInvalidID));
        }

        [Fact]
        public void DeleteWithID_PassObjectWithIDThatDoesntExist_ThrowsException()
        {
            IRepository<TestObject> service = GetService();

            int idThatDoesntExistInDataSource = 999999;

            Assert.Throws<ArgumentException>(() => service.Delete(idThatDoesntExistInDataSource));
        }


        [Fact]
        public void DeleteWithID_PassObjectWithIDThatDoesExist_ItemDeleted()
        {
            IRepository<TestObject> service = GetService();
            int idThatExists = 2;

            service.Delete(idThatExists);
            Assert.Null(service.GetByID(idThatExists));
        }


        // TEST:GetAll()

        [Fact]
        public void GetAll_NonNullObjectReturned()
        {
            IRepository<TestObject> service = GetService();
            Assert.NotNull(service.GetAll());
        }

    }
}
