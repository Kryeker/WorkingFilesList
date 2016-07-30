﻿// WorkingFilesList
// Visual Studio extension tool window that shows a selectable list of files
// that are open in the editor
// Copyright(C) 2016 Anthony Fung

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program. If not, see<http://www.gnu.org/licenses/>.

using NUnit.Framework;
using System.Collections.Generic;
using System.Windows.Data;
using WorkingFilesList.ToolWindow.Model;
using WorkingFilesList.ToolWindow.Model.SortOption;

namespace WorkingFilesList.ToolWindow.Test.Model.SortOption
{
    [TestFixture]
    public class AlphabeticalSortTests
    {
        private static DocumentMetadata CreateDocumentMetadata(string displayName)
        {
            var info = new DocumentMetadataInfo();
            var metadata = new DocumentMetadata(info, string.Empty)
            {
                DisplayName = displayName
            };

            return metadata;
        }

        [Test]
        public void BeginningOfAlphabetAppearsAtStartOfSortedList()
        {
            // Arrange

            const string displayNameA = "A";

            var metadataList = new List<DocumentMetadata>
            {
                CreateDocumentMetadata("B"),
                CreateDocumentMetadata("C"),
                CreateDocumentMetadata(displayNameA)
            };

            var sortOption = new AlphabeticalSort();
            var sortDescription = sortOption.GetSortDescription();
            var view = new ListCollectionView(metadataList);

            // Act

            view.SortDescriptions.Add(sortDescription);

            // Assert

            view.MoveCurrentToFirst();
            var firstItem = (DocumentMetadata) view.CurrentItem;

            Assert.That(firstItem.DisplayName, Is.EqualTo(displayNameA));
        }

        [Test]
        public void HasSortDescriptionIsTrue()
        {
            // Arrange

            var sortOption = new AlphabeticalSort();

            // Assert

            Assert.IsTrue(sortOption.HasSortDescription);
        }

        [TestCase(ProjectItemType.Document, true)]
        [TestCase(ProjectItemType.Project, true)]
        public void ApplicableTypesAreCorrect(ProjectItemType type, bool isApplicable)
        {
            // Arrange

            var sortOption = new AlphabeticalSort();

            // Act

            var hasFlag = sortOption.ApplicableTypes.HasFlag(type);

            // Assert

            Assert.That(hasFlag, Is.EqualTo(isApplicable));
        }
    }
}
